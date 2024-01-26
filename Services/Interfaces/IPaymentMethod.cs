using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IPaymentMethod
    {
        Task<Order> Pay(decimal paymentValue, Customer customer);
    }
}
