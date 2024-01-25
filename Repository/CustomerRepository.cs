using ProvaPub.Models;
using ProvaPub.Repository.intefaces;

namespace ProvaPub.Repository
{
    public class CustomerRepository : BaseRepository<Customer> , ICustomerRepository
    {
        public CustomerRepository(TestDbContext context) : base(context)
        {
        }

    }
}
