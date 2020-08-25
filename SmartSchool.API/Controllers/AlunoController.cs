using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;

        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAlunos(true);
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null)
            {
                return BadRequest($"O aluno {id} não foi encontrado.");
            }

            return Ok(aluno);
        }

        [HttpGet("disciplina/{disciplinaId}")]
        public IActionResult GetByDisciplinaId(int disciplinaId)
        {
            var alunos = _repository.GetAlunosByDisciplinaId(disciplinaId);
            return Ok(alunos);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Não foi possível cadastrar o aluno.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var updateAluno = _repository.GetAlunoById(id);
            if (updateAluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Não foi possível atualizar o aluno.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var updateAluno = _repository.GetAlunoById(id);
            if (updateAluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Não foi possível atualizar o aluno.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeAluno = _repository.GetAlunoById(id);
            if (removeAluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            _repository.Delete(removeAluno);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno removido.");
            }

            return BadRequest("Não foi possível remover o aluno.");
        }
    }
}