using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductList> GetAllAsync(int page,  int pageSize);
    }
}
