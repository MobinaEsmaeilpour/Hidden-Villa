using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IRoomOrderDetailsRepository
    {
        public Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details);
        public Task<RoomOrderDetailsDTO> MarkPaymentsuccessful(int id);
        public Task<RoomOrderDetailsDTO> GetRoomOrderDetail(int roomOrderId);
        public Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetail();
        public Task<bool> UpdateOrderStatus(int RoomOrderId, string status);
        public Task<bool> IsRoomBooked(int RoomId, DateTime chechInDate, DateTime chechOutDate);
    }
}
