using AutoMapper;
using Minifutbol.BL.DTO;
using Minifutbol.BL.Models.Point;
using Minifutbol.DAL.Context;

namespace Minifutbol.BL.Mapping.Profiles
{
    public class PointProfile : Profile
    {
        public PointProfile()
        {
            CreateMap<PointCreateModel, Point>();
            CreateMap<Point, PointViewModel>();
            CreateMap<Point, PointDto>();
            CreateMap<Point, PointCreateModel>();
            CreateMap<PointCreateModel, Point>();
            CreateMap<Point, PointUpdateModel>();
            CreateMap<PointViewModel, PointUpdateModel>();
            CreateMap<PointUpdateModel, Point>();
            CreateMap<Point, PointDeleteModel>();
            CreateMap<PointDeleteModel, Point>();
        }
       
    }
}
