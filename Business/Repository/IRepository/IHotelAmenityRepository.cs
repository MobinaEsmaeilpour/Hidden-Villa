using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelAmenityRepository
    {
        public Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO);
        public Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityid, HotelAmenityDTO hotelAmenityDTO);
        public Task<HotelAmenityDTO> GetHotelAmenity(int amenityid);
        public Task<int> DeleteHotelAmenity(int amenityid);
        public Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenities();
        public Task<HotelAmenityDTO> IsAmenityUnique(string name, int amenityid = 0);
    }
}
