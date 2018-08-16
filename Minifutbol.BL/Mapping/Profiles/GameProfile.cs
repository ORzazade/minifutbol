using AutoMapper;
using Minifutbol.BL.DTO;
using Minifutbol.BL.Models.Game;
using Minifutbol.DAL.Context;

namespace Minifutbol.BL.Mapping.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameCreateModel, Game>();
            CreateMap<Game, GameViewModel>();
            CreateMap<Game, GameDto>();
            CreateMap<Game, GameCreateModel>();
            CreateMap<GameCreateModel, Game>();
            CreateMap<Game, GameUpdateModel>();
            CreateMap<GameViewModel, GameUpdateModel>();
            CreateMap<GameUpdateModel, Game>();
            CreateMap<Game, GameDeleteModel>();
            CreateMap<GameDeleteModel, Game>();
        }
       
    }
}
