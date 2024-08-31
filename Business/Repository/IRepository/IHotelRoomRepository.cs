using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomid, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoom(int roomid, string checkInDate = null, string checkOutDate = null);
        public Task<int> DeleteHotelRoom(int roomid);
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDate=null, string checkOutDate=null);
        public Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);
    }
}
