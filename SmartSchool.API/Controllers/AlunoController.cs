using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public IEnumerable<Aluno> Alunos = new[]
        {
            new Aluno { Id = 1, Nome = "Aluno01", Sobrenome = "Sobrenome01", Telefone = "(123) 45678900" },
            new Aluno { Id = 2, Nome = "Aluno02", Sobrenome = "Sobrenome02", Telefone = "(123) 56789001" },
            new Aluno { Id = 3, Nome = "Aluno03", Sobrenome = "Sobrenome03", Telefone = "(123) 67890012" },
            new Aluno { Id = 4, Nome = "Aluno04", Sobrenome = "Sobrenome04", Telefone = "(123) 78900123" },
            new Aluno { Id = 5, Nome = "Aluno05", Sobrenome = "Sobrenome05", Telefone = "(123) 89001234" }
        };
        
        public AlunoController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return BadRequest($"O aluno {id} não foi encontrado.");
            }

            return Ok(aluno);
        }

        [HttpGet]
        [Route("{nome}/{sobrenome}")] // api/aluno/Aluno01/Sobrenome01
        //[Route("ByName")] // api/aluno/ByName?nome=Aluno01&sobrenome=Sobrenome01
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null)
            {
                return BadRequest($"O aluno {nome} {sobrenome} não foi encontrado.");
            }

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}