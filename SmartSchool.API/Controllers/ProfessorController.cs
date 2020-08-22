using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartSchoolContext _context;

        public ProfessorController(SmartSchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null)
            {
                return BadRequest($"O professor {id} não foi encontrado.");
            }

            return Ok(professor);
        }

        [HttpGet("{nome}")] // api/professor/Professor01
        //[HttpGet("ByName")] // api/professor/ByName?nome=Professor01
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(nome));
            if (professor == null)
            {
                return BadRequest($"O professor {nome} não foi encontrado.");
            }

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var updateProfessor = _context.Professores.AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (updateProfessor == null)
            {
                return BadRequest("Professor não encontrado");
            }

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var updateProfessor = _context.Professores.AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (updateProfessor == null)
            {
                return BadRequest("Professor não encontrado");
            }

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeProfessor = _context.Professores.AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (removeProfessor == null)
            {
                return BadRequest("Professor não encontrado");
            }

            _context.Remove(removeProfessor);
            _context.SaveChanges();

            return Ok();
        }
    }
}