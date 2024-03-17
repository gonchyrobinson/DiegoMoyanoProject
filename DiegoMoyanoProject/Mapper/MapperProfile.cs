using AutoMapper;
using DiegoMoyanoProject.Models;
using DiegoMoyanoProject.ViewModels.Login;
using DiegoMoyanoProject.ViewModels.User;

namespace DiegoMoyanoProject.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<User, CreateUserViewModel>().
            ReverseMap();

            CreateMap<User, UpdateUserViewModel>().
            ReverseMap();

            CreateMap<User,UserOfIndexUserViewModel>().
            ReverseMap();
        }
    }
}
