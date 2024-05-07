﻿using Application.Booking.Dtos;
using Application.Booking.Responses;
using Application.Payment.Responses;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(BookingDto booking);
        Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequest);
        Task<BookingDto> GetBooking(int id);
    }
}
