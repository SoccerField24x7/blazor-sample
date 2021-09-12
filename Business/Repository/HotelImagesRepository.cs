using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelImagesRepository : IHotelImagesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HotelImagesRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
        {
            var image = _mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
            await _context.AddAsync(image);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageById(int imageId)
        {
            var image = await _context.HotelRoomImages.FindAsync(imageId);
            _context.HotelRoomImages.Remove(image);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImagesByRoomId(int roomId)
        {
            var imageList = await _context.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            _context.HotelRoomImages.RemoveRange(imageList);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDTO>>(
                await _context.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync()
            );
        }
    }
}