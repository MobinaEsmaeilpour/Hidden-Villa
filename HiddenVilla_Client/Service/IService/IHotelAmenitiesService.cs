using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IHotelAmenitiesService
    {
        public Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenitiesAsync();
    }
}
