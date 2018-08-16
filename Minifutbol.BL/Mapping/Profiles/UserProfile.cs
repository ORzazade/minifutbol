using AutoMapper;
using Minifutbol.BL.DTO;
using Minifutbol.BL.Models.User;
using Minifutbol.DAL.Context;

namespace Minifutbol.BL.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserCreateModel>();
            CreateMap<UserCreateModel, User>();
            CreateMap<User, UserUpdateModel>();
            CreateMap<UserViewModel, UserUpdateModel>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<User, UserDeleteModel>();
            CreateMap<UserDeleteModel, User>();
        }
       
    }
}
