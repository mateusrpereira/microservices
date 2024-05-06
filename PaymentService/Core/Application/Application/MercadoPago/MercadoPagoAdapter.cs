using Application.MercadoPago.Exceptions;
using Application.Payment;
using Application.Payment.Dtos;
using Application.Payment.Responses;

namespace Application.MercadoPago
{
    public class MercadoPagoAdapter : IMercadoPagoPaymentService
    {
        public Task<PaymentResponse> PayBankTransfer(string paymentIntention)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> PayWithCreditCard(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrEmpty(paymentIntention))
                {
                    throw new InvalidPaymentIntentionException();
                }

                paymentIntention = "/success";

                var dto = new PaymentStateDto()
                {
                    CreatedDate = DateTime.UtcNow,
                    Message = $"Successfully paid {paymentIntention}",
                    PaymentId = "123",
                    Status = Status.Succes,
                };

                var response = new PaymentResponse
                {
                    Data = dto,
                    Success = true,
                    Message = "Payment successfully processed",
                };

                return Task.FromResult(response);
            }
            catch (InvalidPaymentIntentionException)
            {
                var res = new PaymentResponse() 
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION,
                };

                return Task.FromResult(res);
            }
        }

        public Task<PaymentResponse> PayWithDebitCard(string paymentIntention)
        {
            throw new NotImplementedException();
        }
    }
}
