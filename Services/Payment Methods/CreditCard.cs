using ProvaPub.Models;
using ProvaPub.Services.Interfaces;


namespace ProvaPub.Services.Payment_Methods
{
    public class CreditCard : IPaymentMethod
    {
        public async Task<Order> Pay(decimal paymentValue, Customer customer)
        {
            return await Task.FromResult(new Order()
            {
                Value = paymentValue,
                CustomerId = customer.Id,
                OrderDate = DateTime.UtcNow,
                Customer = customer,
            });
        }
    }
}
