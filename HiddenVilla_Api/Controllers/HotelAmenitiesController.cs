using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAmenitiesController : Controller
    {
        private readonly IHotelAmenityRepository _hotelAmenityRepository;

        public HotelAmenitiesController(IHotelAmenityRepository hotelAmenityRepository)
        {
            _hotelAmenityRepository = hotelAmenityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelAmenities()
        {
            var Amenities = await _hotelAmenityRepository.GetAllHotelAmenities();
            return Ok(Amenities);
        }
    }
}
