using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SmartSchool.API.Data;
using SmartSchool.API.Hubs;

namespace SmartSchool.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do MySQL:
            // Adicionar pacote:
            // > dotnet add package MySql.Data.EntityFrameworkCore
            // Executar migrations:
            // > dotnet ef migrations add InitialConfiguration -o Data/Migrations 
            // > dotnet ef database update
            // Gera script inicial do banco de dados, especificando o arquivo e diretório
            // > dotnet ef migrations script -o scripts/init.sql

            services.AddDbContext<SmartSchoolContext>(options => 
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(options => 
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options => 
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider()
                .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options => 
            {
                foreach (var apiVersionDescription in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(apiVersionDescription.GroupName, new OpenApiInfo
                    {
                        Title = "SmartSchool API",
                        Version = apiVersionDescription.ApiVersion.ToString(),
                        Description = "Descrição da WebApi da SmartSchool",
                        Contact = new OpenApiContact
                        {
                            Name = "Nelson Jr",
                            Email = "jr.1403@hotmail.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "SmartSchool License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlCommentsFullPath = Path.Combine(Environment.CurrentDirectory, xmlCommentsFile);

                    options.IncludeXmlComments(xmlCommentsFullPath);
                }
            });

            services.AddSignalR();

            services.AddHealthChecks()
                .AddCheck("Self", () => HealthCheckResult.Healthy())
                .AddMySql(Configuration.GetConnectionString("DefaultConnection"), "SmartSchool.API Database")
                .AddSignalRHub(Configuration["SignalR:Url"], "SmartSchool.API SignalR");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
    
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseSwagger()
                .UseSwaggerUI(options => 
                {
                    foreach (var apiVersionDescription in apiProviderDescription.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json", 
                            apiVersionDescription.GroupName.ToUpperInvariant());
                    }
                    
                    options.RoutePrefix = "";
                });

            // http://localhost:5000/hc
            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/hub");
            });
        }
    }
}
