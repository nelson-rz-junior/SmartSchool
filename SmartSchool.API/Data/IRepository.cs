using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool SaveChanges();

        Aluno[] GetAlunos(bool includeProfessor = false);

        Task<PageList<Aluno>> GetAlunosAsync(PageParameters parameters, bool includeProfessor = false);

        Aluno[] GetAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);

        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

        Professor[] GetProfessores(bool includeAlunos = false);

        Professor[] GetProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);

        Task<Professor[]> GetProfessoresByAlunoIdAsync(int alunoId);

        Professor GetProfessorById(int professorId, bool includeAlunos = false);
    }
}