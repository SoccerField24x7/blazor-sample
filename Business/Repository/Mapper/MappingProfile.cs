using AutoMapper;
using DataAccess.Data;
using Models;

namespace Business.Repository.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>().ReverseMap();
            CreateMap<HotelRoomImageDTO, HotelRoomImage>().ReverseMap();
        }
    }
}