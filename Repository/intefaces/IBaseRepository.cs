﻿namespace ProvaPub.Repository.intefaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);
        Task<int> GetTotaItems();
    }
}