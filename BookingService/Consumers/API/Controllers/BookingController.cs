﻿using Application;
using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Payment.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;
        public BookingController(
            ILogger<BookingController> logger,
            IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("{bookingId}/Pay")]
        public async Task<ActionResult<PaymentResponse>> Pay(
            PaymentRequestDto paymentRequestDto, int bookingId)
        {
            paymentRequestDto.BookingId = bookingId;
            var res = await _bookingManager.PayForABooking(paymentRequestDto);

            if (res.Success) return Ok(res.Data);
            
            return BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var res = await _bookingManager.CreateBooking(booking);

            if (res.Success) return Created("", res.Data);

            else if (res.ErrorCode == ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION)
            {
                //res.Message = _tradutor.Traduzir(res.Message);
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.BOOKING_COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.BOOKING_ROOM_CANNOT_BE_BOOKED)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
    }
}
