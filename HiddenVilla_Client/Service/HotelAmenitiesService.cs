using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class HotelAmenitiesService : IHotelAmenitiesService
    {
        private readonly HttpClient _client;

        public HotelAmenitiesService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenitiesAsync()
        {
            var response = await _client.GetAsync($"api/hotelamenities");
            var content = await response.Content.ReadAsStringAsync();
            var amenities = JsonConvert.DeserializeObject<IEnumerable<HotelAmenityDTO>>(content);
            return amenities;
        }
    }
}
