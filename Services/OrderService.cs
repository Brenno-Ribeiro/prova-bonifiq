using ProvaPub.Models;
using ProvaPub.Services.Exeptions;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;

        public OrderService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, Customer customer)
        {

            var paymentMethods = _paymentService.GeneratePaymentMethods();

            if (paymentMethods.TryGetValue(paymentMethod.ToLower().Trim(), out var payments))
            {
                return await payments.Pay(paymentValue, customer);
            }
            else
            {
                throw new UnprocessedPayment("Pagamento não realizado ou a forma de pagamento não é suportada");
            }

        }

    }

    
}
