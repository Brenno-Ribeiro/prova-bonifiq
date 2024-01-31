using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerList> GetAllAsync(int page,  int pageSize);
        Task<Customer> GetById(int id);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
        Task<bool> VerifyPurchaseInMonth(int customerId , DateTime referenceDate);
        Task<bool> VerifyHaveAlreadyPurchasedBefore(int customerId, decimal purchaseValue);
        bool VerifyValue(decimal numero);
        bool VerifyValue(int numero);


    }
}
