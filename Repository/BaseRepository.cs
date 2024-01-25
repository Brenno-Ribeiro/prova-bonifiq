using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository.intefaces;

namespace ProvaPub.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TestDbContext _context;

        public BaseRepository(TestDbContext context) 
        { 
            _context = context; 
        }

        public async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize)
        {
            var result = _context.Set<T>().AsQueryable();

            return await result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<int> GetTotaItems()
        {
            var result = _context.Set<T>().AsQueryable();
            return await result.CountAsync();
        }
    }
}
