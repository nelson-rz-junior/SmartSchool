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
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IMapper _mapper;

        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetProfessores(true);
            var professoresResult = _mapper.Map<IEnumerable<ProfessorDto>>(professores);

            return Ok(professoresResult);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null)
            {
                return BadRequest($"O professor {id} não foi encontrado.");
            }

            var professorResult = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorResult);
        }

        [HttpGet("disciplina/{disciplinaId}")]
        public IActionResult GetByDisciplinaId(int disciplinaId)
        {
            var professores = _repository.GetProfessoresByDisciplinaId(disciplinaId);
            var professoresResult = _mapper.Map<IEnumerable<ProfessorDto>>(professores);

            return Ok(professoresResult);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Created($"api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Não foi possível cadastrar o professor.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var updateProfessor = _repository.GetProfessorById(id);
            if (updateProfessor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            var professor =_mapper.Map(model, updateProfessor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Created($"api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Não foi possível atualizar o professor.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var updateProfessor = _repository.GetProfessorById(id);
            if (updateProfessor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            var professor =_mapper.Map(model, updateProfessor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Created($"api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor));
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