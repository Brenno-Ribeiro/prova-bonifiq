using ProvaPub.Services.Interfaces;
using ProvaPub.Services.Payment_Methods;

namespace ProvaPub.Services
{
    public class PaymentService : IPaymentService
    {
        public Dictionary<string, IPaymentMethod> GeneratePaymentMethods()
        {
            var paymentMethods = new Dictionary<string, IPaymentMethod>
            {
                { "pix", new Pix() },
                { "creditcard", new CreditCard() },
                { "paypal", new PayPal() }
            };


            return paymentMethods;
        }

    }
}
