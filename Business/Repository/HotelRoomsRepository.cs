//using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Business.Repository
{
    public class HotelRoomsRepository : IHotelRoomRepository
    {
        private readonly AppilicationDbContext _db;
        //private readonly IMapper _mapper;
        //adding rooms for hotel
        public HotelRoomsRepository(AppilicationDbContext db)
            //IMapper mapper)
        {
            //_mapper = mapper;
            _db = db; 
        }
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            // Manually map HotelRoomDTO to HotelRoom
            HotelRoom hotelRoom = new HotelRoom
            {
                Id = hotelRoomDTO.Id,
                Name = hotelRoomDTO.Name,
                RegularRate = hotelRoomDTO.RegularRate,
                Occupancy = hotelRoomDTO.Occupancy,
                SqFt = hotelRoomDTO.SqFt,
                Detail = hotelRoomDTO.Detail,
                CreatedDate = DateTime.Now,
                CreatedBy = ""
            };
            var addHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();


            HotelRoomDTO resultDTO = new HotelRoomDTO
            {
                Id = addHotelRoom.Entity.Id,
                Name = addHotelRoom.Entity.Name,
                Occupancy = addHotelRoom.Entity.Occupancy,
                SqFt = addHotelRoom.Entity.SqFt,
                Detail = addHotelRoom.Entity.Detail,
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
                var allimages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
                // We delete this part of the code because when we delete a room, the photos of that room in the database are not deleted (if we go to the list of rooms in our hotel, where we actually call it, it does not work).
                //foreach (var image in allimages) 
                //{
                //    if(File.Exists(image.RoomImageUrl))
                //    {
                //        File.Delete(image.RoomImageUrl);
                //    }
                //}
                _db.HotelRoomImages.RemoveRange(allimages);
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

                var hotelRooms = await _db.HotelRooms.Include(x => x.HotelRoomImages).ToListAsync();


                var hotelRoomDTOs = hotelRooms.Select(hotelroom => new HotelRoomDTO
                {
                    Id = hotelroom.Id,
                    Name = hotelroom.Name,
                    Occupancy = hotelroom.Occupancy,
                    RegularRate = hotelroom.RegularRate,
                    SqFt = hotelroom.SqFt,
                    Detail = hotelroom.Detail,
                    ImageUrls = hotelroom.HotelRoomImages.Select(img => img.RoomImageUrl).ToList()
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
                var hotelRoom = await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId);

                if (hotelRoom == null)
                {
                    return null;
                }

                var hotelRoomDTO = new HotelRoomDTO
                {
                    Id = hotelRoom.Id,
                    Name = hotelRoom.Name,
                    Occupancy = hotelRoom.Occupancy,
                    RegularRate = hotelRoom.RegularRate,
                    Detail = hotelRoom.Detail,
                    SqFt = hotelRoom.SqFt,
                    ImageUrls = hotelRoom.HotelRoomImages.Select(img => img.RoomImageUrl).ToList(),
                    HotelRoomImages = hotelRoom.HotelRoomImages.Select(img => new HotelRoomImageDTO
                    {
                        Id=roomId,
                        RoomId = roomId,
                        RoomImageUrl = img.RoomImageUrl,
                    }).ToList()
                };

                return hotelRoomDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        //{
        //    try
        //    {
        //        HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom,HotelRoomDTO>(
        //            await _db.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId));
        //        return hotelRoom;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

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
        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
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
                else
                {
                    var hotelRoomEntity = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                    && x.Id != roomId);
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
                HotelRoom roomDetails = await _db.HotelRooms.FindAsync(hotelRoomDTO.Id);

                if (roomDetails != null)
                {
                    roomDetails.Name = hotelRoomDTO.Name;
                    roomDetails.Occupancy = hotelRoomDTO.Occupancy;
                    roomDetails.RegularRate = hotelRoomDTO.RegularRate;
                    roomDetails.Detail = hotelRoomDTO.Detail;
                    roomDetails.SqFt = hotelRoomDTO.SqFt;
                    roomDetails.UpdatedDate = DateTime.Now;

                    if(hotelRoomDTO.ImageUrls != null && hotelRoomDTO.ImageUrls.Any())
                    {
                        foreach (var imageUrl in hotelRoomDTO.ImageUrls)
                        {
                            if (!roomDetails.HotelRoomImages.Any(img => img.RoomImageUrl == imageUrl))
                            {
                                roomDetails.HotelRoomImages.Add(new HotelRoomImage
                                {
                                    RoomImageUrl = imageUrl,
                                    RoomId = roomDetails.Id
                                });
                            }
                        }
                    }

                    _db.HotelRooms.Update(roomDetails);
                    await _db.SaveChangesAsync();

                    return new HotelRoomDTO
                    {
                        Id = roomDetails.Id,
                        Name = roomDetails.Name,
                        Occupancy = roomDetails.Occupancy,
                        RegularRate = roomDetails.RegularRate,
                        Detail = roomDetails.Detail,
                        SqFt = roomDetails.SqFt,
                        ImageUrls = roomDetails.HotelRoomImages.Select(img => img.RoomImageUrl).ToList()
                    };
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

        //    public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        //    {
        //        try
        //        {
        //            if (roomId == hotelRoomDTO.Id)
        //            {
        //                //Ivalid
        //                HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
        //                HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
        //                room.UpdateedBy = "";
        //                room.UpdatedDate = DateTime.Now;
        //                var updateedRoom = _db.HotelRooms.Update(room);
        //                await _db.SaveChangesAsync();
        //                return _mapper.Map<HotelRoom, HotelRoomDTO>(updateedRoom.Entity);
        //            }
        //            else
        //            {
        //                //invalid
        //                return null;
        //            }
        //        }
        //        catch (Exception ex) 
        //        {
        //            return null;
        //        }

        //    }
        //}
    
}
