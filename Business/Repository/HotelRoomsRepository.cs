using AutoMapper;
using Business.Repository.IRepositpry;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Business.Repository
{
    public class HotelRoomsRepository : IHotelRoomRepository
    {
        private readonly AppilicationDbContext _db;
        private readonly IMapper _mapper;
        //adding rooms for hotel
        public HotelRoomsRepository(AppilicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;;
        }

        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            // Manually map HotelRoomDTO to HotelRoom
            HotelRoom hotelRoom = new HotelRoom
            {
                Id = hotelRoomDTO.Id,
                Name = hotelRoomDTO.Name,
                Occupancy = hotelRoomDTO.Occupancy,
                SqFt = hotelRoomDTO.SqFt,
                Detail = hotelRoomDTO.Detail,
                // Map other properties as needed
                CreatedDate = DateTime.Now,
                CreatedBy = ""
            };
            var addHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            

            // Manually map HotelRoom to HotelRoomDTO
            HotelRoomDTO resultDTO = new HotelRoomDTO
            {
                Id = addHotelRoom.Entity.Id,
                Name = addHotelRoom.Entity.Name,
                Occupancy = addHotelRoom.Entity.Occupancy,
                SqFt = addHotelRoom.Entity.SqFt,
                Detail= addHotelRoom.Entity.Detail,
                // Map other properties as needed
            };

            return resultDTO;
        }

        //public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        //{
        //    HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
        //    hotelRoom.CreatedDate = DateTime.Now;
        //    hotelRoom.CreatedBy = "";
        //    var addHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
        //    await _db.SaveChangesAsync();
        //    return _mapper.Map<HotelRoom, HotelRoomDTO>(addHotelRoom.Entity);
        //}

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        //public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        //{
        //    try
        //    {
        //        IEnumerable<HotelRoomDTO> hotelRoomDTOs =
        //            _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_db.HotelRooms);
        //        return hotelRoomDTOs;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            try
            {
                // Retrieve hotel rooms from the database
                var hotelRooms =  await _db.HotelRooms.ToListAsync();

                // Convert to DTOs
                var hotelRoomDTOs = hotelRooms.Select(hotelroom => new HotelRoomDTO
                {
                    Id = hotelroom.Id,
                    Name = hotelroom.Name,
                    Occupancy = hotelroom.Occupancy,
                    RegularRate = hotelroom.RegularRate,
                    SqFt = hotelroom.SqFt,
                    Detail= hotelroom.Detail,
                });

                return hotelRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom,HotelRoomDTO>(
                    await _db.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId));
                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //if unique returns null else returns the room obj
        //public async Task<HotelRoomDTO> IsRoomUnique(string name)
        //{
        //    try
        //    {
        //        //var w = await _db
        //        //    .HotelRooms
        //        //    .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());


        //        //return new HotelRoomDTO();
        //        HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
        //            await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
        //        return hotelRoom;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public async Task<HotelRoomDTO> IsRoomUnique(string name)
        {
            try
            {
                var hotelRoomEntity = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                if (hotelRoomEntity == null)
                {
                    return null;
                }

                var hotelRoomDTO = new HotelRoomDTO
                {
                    Id = hotelRoomEntity.Id,
                    Name = hotelRoomEntity.Name,
                    
                };

                return hotelRoomDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsRoomUnique: {ex.Message}");
                return null;
            }
        }


        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //Ivalid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdateedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updateedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updateedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex) 
            {
                return null;
            }
           
        }
    }
}
