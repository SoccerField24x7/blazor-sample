using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelImagesRepository
    {
        public Task<int> CreateHotelRoomImage(HotelRoomImageDTO image);
        public Task<int> DeleteHotelRoomImageById(int imageId);
        public Task<int> DeleteHotelRoomImagesByRoomId(int roomId);
        public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId);
    }
}