using AutoMapper;
using Business.Repository.IRepository;
using Common;
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
    public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
    {

        private readonly AppilicationDbContext _db;
        private readonly IMapper _mapper;
            
        public RoomOrderDetailsRepository(AppilicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
        {
            try
            {
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate = details.CheckOutDate.Date;
                var roomOrder = _mapper.Map<RoomOrderDetailsDTO, RoomOrderDetails>(details);
                roomOrder.status = SD.Status_Pending;
                var resualt = await _db.RoomOrderDetails.AddAsync(roomOrder);
                await _db.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(resualt.Entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetail()
        {
            try
            {
                IEnumerable<RoomOrderDetailsDTO> roomOrders = _mapper.Map<IEnumerable<RoomOrderDetails>, IEnumerable<RoomOrderDetailsDTO>>
                    (_db.RoomOrderDetails.Include(u => u.HotelRoom));
                return roomOrders;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<RoomOrderDetailsDTO> GetRoomOrderDetail(int roomOrderId)
        {
            try
            {
                RoomOrderDetails roomOrder = await _db.RoomOrderDetails
                    .Include(u => u.HotelRoom).ThenInclude(x => x.HotelRoomImages)
                    .FirstOrDefaultAsync(u => u.Id == roomOrderId);
                RoomOrderDetailsDTO roomOrderDetailsDTO = _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(roomOrder);
                roomOrderDetailsDTO.HotelRoomDTO.TotalDays = roomOrderDetailsDTO.CheckOutDate.Subtract(roomOrderDetailsDTO.CheckInDate).Days;
                return roomOrderDetailsDTO;    
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int RoomId, DateTime chechInDate, DateTime chechOutDate)
        {
            var status = false;
            var existingBooking = await _db.RoomOrderDetails.Where(x=>x.RoomId==RoomId && x.IsPaymentSuccessful &&
            //check if checkin date that user wants does not fall in between any dates for room that is booked
            (chechInDate<x.CheckOutDate && chechInDate.Date > x.CheckInDate
            //check if checkout date that user wants does not fall in between any dates for room that is booked
            || chechOutDate.Date > x.CheckInDate.Date && chechInDate.Date < x.CheckInDate.Date
            )).FirstOrDefaultAsync();

            if(existingBooking != null)
            {
                status = true;
            }
            return status;
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentsuccessful(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(int RoomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
