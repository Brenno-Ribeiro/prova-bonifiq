using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IProductService
    {
        Task<ProductList> GetAllAsync(int page,  int pageSize);
    }
}
