using AutoMapper;
using Minifutbol.BL.DTO;
using Minifutbol.BL.Models.TeamRequest;
using Minifutbol.DAL.Context;

namespace Minifutbol.BL.Mapping.Profiles
{
    public class TeamRequestProfile : Profile
    {
        public TeamRequestProfile()
        {
            CreateMap<TeamRequestCreateModel, TeamRequest>();
            CreateMap<TeamRequest, TeamRequestViewModel>();
            CreateMap<TeamRequest, TeamRequestDto>();
            CreateMap<TeamRequest, TeamRequestCreateModel>();
            CreateMap<TeamRequestCreateModel, TeamRequest>();
            CreateMap<TeamRequest, TeamRequestUpdateModel>();
            CreateMap<TeamRequestViewModel, TeamRequestUpdateModel>();
            CreateMap<TeamRequestUpdateModel, TeamRequest>();
            CreateMap<TeamRequest, TeamRequestDeleteModel>();
            CreateMap<TeamRequestDeleteModel, TeamRequest>();
        }
       
    }
}
