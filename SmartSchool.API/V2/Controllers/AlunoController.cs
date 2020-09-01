using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.V2.DTOs;
using SmartSchool.API.Models;

namespace SmartSchool.API.V2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Método responsável por retornar os dados de todos os alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAlunos(true);
            var model = _mapper.Map<IEnumerable<AlunoDto>>(alunos);
            
            return Ok(model);
        }

        /// <summary>
        /// Método responsável por retornar os dados de um aluno utilizando o seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por retornar os dados de um aluno utilizando o código da disciplina
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <returns></returns>
        [HttpGet("disciplina/{disciplinaId}")]
        public IActionResult GetByDisciplinaId(int disciplinaId)
        {
            var alunos = _repository.GetAlunosByDisciplinaId(disciplinaId);
            var model = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            return Ok(model);
        }

        /// <summary>
        /// Método responsável por gravar os dados de um aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por atualizar dados de um aluno utilizando seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por atualizar parcialmente os dados de um aluno utilizando seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por remover os dados de um aluno utilizando o seu código identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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