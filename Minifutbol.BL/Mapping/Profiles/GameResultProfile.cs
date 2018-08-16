using AutoMapper;
using Minifutbol.BL.DTO;
using Minifutbol.BL.Models.GameResult;
using Minifutbol.DAL.Context;

namespace Minifutbol.BL.Mapping.Profiles
{
    public class GameResultProfile : Profile
    {
        public GameResultProfile()
        {
            CreateMap<GameResultCreateModel, GameResult>();
            CreateMap<GameResult, GameResultViewModel>();
            CreateMap<GameResult, GameResultDto>();
            CreateMap<GameResult, GameResultCreateModel>();
            CreateMap<GameResultCreateModel, GameResult>();
            CreateMap<GameResult, GameResultUpdateModel>();
            CreateMap<GameResultViewModel, GameResultUpdateModel>();
            CreateMap<GameResultUpdateModel, GameResult>();
            CreateMap<GameResult, GameResultDeleteModel>();
            CreateMap<GameResultDeleteModel, GameResult>();
        }
       
    }
}
