using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerList> GetAllAsync(int page,  int pageSize);
        Task<Customer> GetById(int id);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
