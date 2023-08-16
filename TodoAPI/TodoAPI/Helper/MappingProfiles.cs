using AutoMapper;
using TodoAPI.Dto;
using TodoAPI.Models;

namespace TodoAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() { 
            CreateMap<Todo,TodoDto>();        
        }
    }
}
