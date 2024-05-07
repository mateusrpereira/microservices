﻿using Application;
using Application.Room.Dtos;
using Application.Room.Ports;
using Application.Room.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IRoomManager _roomManager;
        public RoomController(ILogger<GuestController> logger, IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> Post(RoomDto room)
        {
            var request = new CreateRoomRequest
            {
                Data = room
            };

            var res = await _roomManager.CreateRoom(request);

            if (res.Success) return Created("", res.Data);
            
            else if(res.ErrorCode == ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            else if(res.ErrorCode == ErrorCodes.ROOM_COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("");
            return BadRequest(500);
        }
    }
}
