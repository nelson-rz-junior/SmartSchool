using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetProfessores(true);
            return Ok(professores);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null)
            {
                return BadRequest($"O professor {id} não foi encontrado.");
            }

            return Ok(professor);
        }

        [HttpGet("disciplina/{disciplinaId}")]
        public IActionResult GetByDisciplinaId(int disciplinaId)
        {
            var professores = _repository.GetProfessoresByDisciplinaId(disciplinaId);
            return Ok(professores);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Não foi possível cadastrar o professor.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var updateProfessor = _repository.GetProfessorById(id);
            if (updateProfessor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Não foi possível atualizar o professor.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var updateProfessor = _repository.GetProfessorById(id);
            if (updateProfessor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Não foi possível atualizar o professor.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeProfessor = _repository.GetProfessorById(id);
            if (removeProfessor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            _repository.Delete(removeProfessor);
            if (_repository.SaveChanges())
            {
                return Ok("Professor removido.");
            }

            return BadRequest("Não foi possível remover o professor.");
        }
    }
}