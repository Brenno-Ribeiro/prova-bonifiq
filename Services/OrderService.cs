using ProvaPub.Models;
using ProvaPub.Services.Exeptions;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly ICustomerService _customerService;

        public OrderService(IPaymentService paymentService, ICustomerService customerService)
        {
            _paymentService = paymentService;
            _customerService = customerService;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, Customer customer)
        {

            if(!_customerService.VerifyValue(paymentValue)) throw new ValueLessThanOrEqualToZeroException("Value cannot be less than or equal to zero");

            var paymentMethods = _paymentService.GeneratePaymentMethods();

            if (paymentMethods.TryGetValue(paymentMethod.ToLower().Trim(), out var payments))
            {
                return await payments.Pay(paymentValue, customer);
            }
            else
            {
                throw new UnprocessedPaymentException("Pagamento não realizado ou a forma de pagamento não é suportada");
            }

        }

    }

    
}
