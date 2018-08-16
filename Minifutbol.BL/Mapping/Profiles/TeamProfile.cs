using AutoMapper;
using Minifutbol.BL.DTO;
using Minifutbol.BL.Models.Team;
using Minifutbol.DAL.Context;

namespace Minifutbol.BL.Mapping.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamCreateModel, Team>();
            CreateMap<Team, TeamViewModel>();
            CreateMap<Team, TeamDto>();
            CreateMap<Team, TeamCreateModel>();
            CreateMap<TeamCreateModel, Team>();
            CreateMap<Team, TeamUpdateModel>();
            CreateMap<TeamViewModel, TeamUpdateModel>();
            CreateMap<TeamUpdateModel, Team>();
            CreateMap<Team, TeamDeleteModel>();
            CreateMap<TeamDeleteModel, Team>();
        }
       
    }
}
