using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Business.Repository.IRepository;
using DataAccess.Data;
using AutoMapper;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            
            hotelRoom.CreatedDate = DateTime.UtcNow;
            hotelRoom.CreatedBy = "";

            var addedHotelRoom = await _context.Set<HotelRoom>().AddAsync(hotelRoom);

            await _context.SaveChangesAsync();

            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _context.Set<HotelRoom>().FindAsync(roomId);
            if (roomDetails != null)
            {
                var allImages = await _context.Set<HotelRoomImage>().Where(x => x.RoomId == roomId).ToListAsync();
                foreach(var image in allImages)
                {
                    if(File.Exists(image.RoomImageUrl))
                    {
                        File.Delete(image.RoomImageUrl);
                    }
                }

                _context.RemoveRange(allImages);
                _context.HotelRooms.Remove(roomDetails);
                
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            IEnumerable<HotelRoomDTO> hotelRoomDTOs = null;

            try
            {
                hotelRoomDTOs =
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_context.HotelRooms.Include(x => x.HotelRoomImages));
            }
            catch (Exception ex)
            {
                // TODO: logging
            }

            return hotelRoomDTOs;
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            HotelRoomDTO hotelRoomDTO = null;

            try
            {
                hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(await _context.Set<HotelRoom>().Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));
            }
            catch(Exception ex)
            {
                // TODO: logging
            }

            return hotelRoomDTO;
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId)
        {
            HotelRoomDTO hotelRoomDTO = null;

            try
            {
                if (roomId == 0)
                {
                    hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(await _context.Set<HotelRoom>().FirstOrDefaultAsync(x => x.Name.ToLower() == name.Trim().ToLower()));
                }
                else
                {
                    hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _context.Set<HotelRoom>()
                            .FirstOrDefaultAsync(x => x.Name.ToLower() == name.Trim().ToLower()
                            && x.Id != roomId
                    ));
                }
                
            }
            catch(Exception ex)
            {
                // TODO: logging
            }

            return hotelRoomDTO;
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            HotelRoomDTO updatedRoom = null;

            try {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _context.Set<HotelRoom>().FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.UtcNow;

                    var result = _context.HotelRooms.Update(room).Entity;
                    await _context.SaveChangesAsync();

                    updatedRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(result);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {

            }
            
            return updatedRoom;
        }
    }
}