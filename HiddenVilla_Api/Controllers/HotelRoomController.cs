﻿using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelRooms()
        {
            var allRooms = await _hotelRoomRepository.GetAllHotelRooms();
            return Ok(allRooms);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery]int? roomId)
        {
            if (roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErroroMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value);
            if (roomDetails  == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErroroMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(roomDetails);
        }
    }
}