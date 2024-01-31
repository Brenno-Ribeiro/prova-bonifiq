using System.Linq.Expressions;

namespace ProvaPub.Repository.intefaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);
        Task<T> GetById(int id);
        Task<int> GetTotaItems();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
