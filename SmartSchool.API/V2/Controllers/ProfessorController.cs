using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.V2.DTOs;
using SmartSchool.API.Models;
using System.Threading.Tasks;

namespace SmartSchool.API.V2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IMapper _mapper;

        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Método responsável por retornar os dados de todos os professores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetProfessores(true);
            var professoresResult = _mapper.Map<IEnumerable<ProfessorDto>>(professores);

            return Ok(professoresResult);
        }

        /// <summary>
        /// Método responsável por retornar os dados de um professor utilizando o seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por retornar os dados de um professor utilizando o código da disciplina
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <returns></returns>
        [HttpGet("disciplina/{disciplinaId}")]
        public IActionResult GetByDisciplinaId(int disciplinaId)
        {
            var professores = _repository.GetProfessoresByDisciplinaId(disciplinaId);
            var professoresResult = _mapper.Map<IEnumerable<ProfessorDto>>(professores);

            return Ok(professoresResult);
        }

        /// <summary>
        /// Método responsável por retornar os dados de um professor utilizando o código do aluno
        /// </summary>
        /// <param name="alunoId"></param>
        /// <returns></returns>
        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> GetByAlunoIdAsync(int alunoId)
        {
            var professores = await _repository.GetProfessoresByAlunoIdAsync(alunoId);
            var professoresResult = _mapper.Map<IEnumerable<ProfessorDto>>(professores);

            return Ok(professoresResult);
        }

        /// <summary>
        /// Método responsável por gravar os dados de um professor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por atualizar os dados de um professor utilizando o seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por atualizar parcialmente os dados de um professor utilizando o seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por remover os dados de um professor utilizando o seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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