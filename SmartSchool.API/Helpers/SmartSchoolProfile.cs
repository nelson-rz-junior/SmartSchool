using AutoMapper;
using SmartSchool.API.DTOs;
using SmartSchool.API.Models;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome, 
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));
        }
    }
}