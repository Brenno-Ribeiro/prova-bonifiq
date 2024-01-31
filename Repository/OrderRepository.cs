using ProvaPub.Models;
using ProvaPub.Repository.intefaces;

namespace ProvaPub.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(TestDbContext context) : base(context)
        {
        }
    }
}
