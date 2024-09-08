using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace HiddenVilla_API.Controllers
{
    //Purpose of this controller is to fetch the rooms from the database and return them to the client home page of the application.
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(string checkIn = null, string checkOut = null)
        {
            if (string.IsNullOrEmpty(checkIn) || string.IsNullOrEmpty(checkOut))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }
            //check if the dates are in dd mm yyyy
            if (!DateTime.TryParseExact(checkIn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid date - Please do it as MM/dd/yyyy"
                });
            }
            if (!DateTime.TryParseExact(checkOut, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid date - Please do it as MM/dd/yyyy"
                });
            }

            var RoomCount = await _hotelRoomRepository.GetAllHotelRooms(checkIn, checkOut);
            return Ok(RoomCount);
        }


        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int? roomId, string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            
            var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkInDate, checkOutDate);

            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "Error",
                    ErrorMessage = "InvalidRoomId",
                    StatusCode = StatusCodes.Status404NotFound
                }); ;
            }
            return Ok(roomDetails);
        }
    }
}