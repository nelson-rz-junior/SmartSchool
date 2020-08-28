using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.DTOs;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAlunos(true);
            var model = _mapper.Map<IEnumerable<AlunoDto>>(alunos);
            
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null)
            {
                return BadRequest($"O aluno {id} não foi encontrado.");
            }

            var model = _mapper.Map<AlunoDto>(aluno);

            return Ok(model);
        }

        [HttpGet("disciplina/{disciplinaId}")]
        public IActionResult GetByDisciplinaId(int disciplinaId)
        {
            var alunos = _repository.GetAlunosByDisciplinaId(disciplinaId);
            var model = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"api/aluno/{aluno.Id}",_mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Não foi possível cadastrar o aluno.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var updateAluno = _repository.GetAlunoById(id);
            if (updateAluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            var aluno = _mapper.Map(model, updateAluno);

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"api/aluno/{aluno.Id}",_mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Não foi possível atualizar o aluno.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var updateAluno = _repository.GetAlunoById(id);
            if (updateAluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            var aluno = _mapper.Map(model, updateAluno);

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"api/aluno/{aluno.Id}",_mapper.Map<AlunoDto>(aluno));
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