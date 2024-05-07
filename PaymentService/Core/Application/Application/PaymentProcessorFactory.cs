using Application.Booking.Dtos;
using Application.MercadoPago;
using Application.Payment;

namespace Payments.Application
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders suportedPaymentProviders)
        {
            switch (suportedPaymentProviders)
            {
                case SupportedPaymentProviders.MercadoPago:
                    return new MercadoPagoAdapter();

                default: return new NotImplementedPaymentProvider();
            }
        }
    }
}
