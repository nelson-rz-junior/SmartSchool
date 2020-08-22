using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartSchoolContext _context;

        public AlunoController(SmartSchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return BadRequest($"O aluno {id} não foi encontrado.");
            }

            return Ok(aluno);
        }

        [HttpGet("{nome}/{sobrenome}")] // api/aluno/Aluno01/Sobrenome01
        //[HttpGet("ByName")] // api/aluno/ByName?nome=Aluno01&sobrenome=Sobrenome01
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null)
            {
                return BadRequest($"O aluno {nome} {sobrenome} não foi encontrado.");
            }

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var updateAluno = _context.Alunos.AsNoTracking()
                .FirstOrDefault(a => a.Id == id);

            if (updateAluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _context.Update(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var updateAluno = _context.Alunos.AsNoTracking()
                .FirstOrDefault(a => a.Id == id);

            if (updateAluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _context.Update(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeAluno = _context.Alunos.AsNoTracking()
                .FirstOrDefault(a => a.Id == id);

            if (removeAluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _context.Remove(removeAluno);
            _context.SaveChanges();

            return Ok();
        }
    }
}