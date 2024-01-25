using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerList> GetAllAsync(int page,  int pageSize);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
