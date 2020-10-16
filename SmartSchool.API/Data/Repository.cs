using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartSchoolContext _context;

        public Repository(SmartSchoolContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public Aluno[] GetAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            return query.OrderBy(a => a.Nome)
                .AsNoTracking()
                .ToArray();
        }

        public async Task<PageList<Aluno>> GetAlunosAsync(PageParameters parameters, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.OrderBy(a => a.Nome)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(parameters.Nome))
            {
                query = query.Where(a => EF.Functions.Like(a.Nome, $"%{parameters.Nome}%") || 
                    EF.Functions.Like(a.Sobrenome, $"%{parameters.Nome}%"));
            }

            if (parameters.Matricula.HasValue)
            {
                query = query.Where(a => a.Matricula == parameters.Matricula.Value);
            }

            if (parameters.Status.HasValue)
            {
                query = query.Where(a => a.Ativo == parameters.Status.Value);
            }

            return await PageList<Aluno>.CreateAsync(query, parameters.PageNumber, parameters.PageSize);
        }

        public Aluno[] GetAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
                
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            return query.Where(a => a.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId))
                .OrderBy(a => a.Nome)
                .AsNoTracking()
                .ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
                
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            return query.Where(a => a.Id == alunoId)
                .OrderBy(a => a.Nome)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Professor[] GetProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            return query.OrderBy(p => p.Nome)
                .AsNoTracking()
                .ToArray();
        }

        public async Task<Professor[]> GetProfessoresByAlunoIdAsync(int alunoId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
                
            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            return await query
                .Where(p => p.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.AlunoId == alunoId)))
                .OrderBy(p => p.Nome)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public Professor[] GetProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
                
            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            return query.Where(p => p.Disciplinas.Any(d => d.Id == disciplinaId))
                .OrderBy(p => p.Nome)
                .AsNoTracking()
                .ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
                
            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            return query.Where(p => p.Id == professorId)
                .OrderBy(p => p.Nome)
                .AsNoTracking()
                .FirstOrDefault();
        }
    }
}