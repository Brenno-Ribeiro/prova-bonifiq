using ProvaPub.Models;
using ProvaPub.Repository.intefaces;

namespace ProvaPub.Repository
{
    public class ProductRepository : BaseRepository<Product> , IProductRepository
    {
        public ProductRepository(TestDbContext context) : base(context)
        {
        }

    }
}
