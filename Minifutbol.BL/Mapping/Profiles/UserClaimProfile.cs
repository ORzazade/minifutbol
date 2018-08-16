using AutoMapper;
using Minifutbol.BL.DTO;
using Minifutbol.BL.Models.UserClaim;
using Minifutbol.DAL.Context;

namespace Minifutbol.BL.Mapping.Profiles
{
    public class UserClaimProfile : Profile
    {
        public UserClaimProfile()
        {
            CreateMap<UserClaimCreateModel, UserClaim>();
            CreateMap<UserClaim, UserClaimViewModel>();
            CreateMap<UserClaim, UserClaimDto>();
            CreateMap<UserClaim, UserClaimCreateModel>();
            CreateMap<UserClaimCreateModel, UserClaim>();
            CreateMap<UserClaim, UserClaimUpdateModel>();
            CreateMap<UserClaimViewModel, UserClaimUpdateModel>();
            CreateMap<UserClaimUpdateModel, UserClaim>();
            CreateMap<UserClaim, UserClaimDeleteModel>();
            CreateMap<UserClaimDeleteModel, UserClaim>();
        }
       
    }
}
