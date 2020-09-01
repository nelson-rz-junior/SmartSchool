using AutoMapper;
using SmartSchool.API.V2.DTOs;
using SmartSchool.API.Models;
using SmartSchool.API.Helpers;

namespace SmartSchool.API.V2.Profiles
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome, 
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                .ForMember(
                    dest => dest.Idade, 
                    opt => opt.MapFrom(src => src.DataNascimento.GetCurrentAge()));

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    dest => dest.Nome, 
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));

            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();
        }
    }
}