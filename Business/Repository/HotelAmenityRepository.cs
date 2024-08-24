using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelAmenityRepository : IHotelAmenityRepository
    {
        private readonly AppilicationDbContext _db;

        public HotelAmenityRepository(AppilicationDbContext db)
        {
            _db = db;
        }
        public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO)
        {
            HotelAmenity hotelAmenity = new HotelAmenity
            {
                Id = hotelAmenityDTO.Id,
                Name = hotelAmenityDTO.Name,
                Description = hotelAmenityDTO.Description,
                Timming = hotelAmenityDTO.Timming,
                IconStyle = hotelAmenityDTO.IconStyle,
                CreatedDate = DateTime.Now,
                CreatedBy = ""
            };
            var addhotelAmenity = await _db.HotelAmenities.AddAsync(hotelAmenity);    
            await _db.SaveChangesAsync();

            HotelAmenityDTO resultAmenityDTO = new HotelAmenityDTO
            {
                Id = addhotelAmenity.Entity.Id,
                Name = addhotelAmenity.Entity.Name,
                Description = addhotelAmenity.Entity.Description,
                Timming = addhotelAmenity.Entity.Timming,
                IconStyle = addhotelAmenity.Entity.IconStyle,

            };
            return resultAmenityDTO;
        }

        public async Task<int> DeleteHotelAmenity(int amenityid)
        {
            var amenityDetails = await _db.HotelAmenities.FindAsync(amenityid);
            if (amenityDetails != null) 
            {
                _db.HotelAmenities.Remove(amenityDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenities()
        {
            try
            {
                var hotelAmenities = await _db.HotelAmenities.ToListAsync();
                var hotelamenityDTOs = hotelAmenities.Select(hotelAmenities => new HotelAmenityDTO
                {
                    Id = hotelAmenities.Id, 
                    Name = hotelAmenities.Name,
                    Description = hotelAmenities.Description,
                    Timming = hotelAmenities.Timming,
                    IconStyle = hotelAmenities.IconStyle,
                });
                return hotelamenityDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> GetHotelAmenity(int amenityid)
        {
            try
            {
                var hotelAmenity = await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Id == amenityid);

                if (hotelAmenity == null)
                {
                    return null;
                }

                var hotelamenityDTO = new HotelAmenityDTO
                {
                    Id = hotelAmenity.Id,
                    Name = hotelAmenity.Name,
                    Description = hotelAmenity.Description,
                    Timming = hotelAmenity.Timming,
                    IconStyle = hotelAmenity.IconStyle,
                };
                return hotelamenityDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> IsAmenityUnique(string name, int amenityid = 0)
        {
            try
            {
                if (amenityid == 0)
                {
                    var hotelAmenityEntity = await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                    if (hotelAmenityEntity == null)
                    {
                        return null;
                    }

                    var hotelAmenityDTO = new HotelAmenityDTO
                    {
                        Id = hotelAmenityEntity.Id,
                        Name = hotelAmenityEntity.Name,
                    };
                    return hotelAmenityDTO;
                }
                else
                {
                    var hotelAmenityEntity = await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                    && x.Id != amenityid);
                    if (hotelAmenityEntity == null)
                    {
                        return null;
                    }
                    var hotelAmenityDTO = new HotelAmenityDTO
                    {
                        Id = hotelAmenityEntity.Id,
                        Name = hotelAmenityEntity.Name,
                    };
                    return hotelAmenityDTO;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsAminityUnique: {ex.Message}");
                return null;
            }
        }

        public async Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityid, HotelAmenityDTO hotelAmenityDTO)
        {
            try
            {
                HotelAmenity amenityDetails = await _db.HotelAmenities.FindAsync(hotelAmenityDTO.Id);

                if (amenityDetails != null)
                {
                    amenityDetails.Name = hotelAmenityDTO.Name;
                    amenityDetails.Id = hotelAmenityDTO.Id;
                    amenityDetails.Description = hotelAmenityDTO.Description;
                    amenityDetails.Timming = hotelAmenityDTO.Timming;
                    amenityDetails.IconStyle = hotelAmenityDTO.IconStyle;
                    amenityDetails.UpdatedDate = DateTime.Now;

                    _db.HotelAmenities.Update(amenityDetails);
                    await _db.SaveChangesAsync();

                    return new HotelAmenityDTO
                    {
                        Id = amenityDetails.Id,
                        Name = amenityDetails.Name,
                        Description = amenityDetails.Description,
                        Timming  = amenityDetails.Timming,
                        IconStyle = amenityDetails.IconStyle,
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
}
