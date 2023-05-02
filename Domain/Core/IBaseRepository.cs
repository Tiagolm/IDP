using System.Collections.Generic;

namespace Domain.Core
{
    public interface IBaseRepository<T> where T : Entity
    {
        IQueryable<T> Query { get; }

        Task<IEnumerable<T>> List();

        Task<T> GetById(int id);

        Task<T> Add(T obj);

        Task Update(T contact);

        Task Delete(int id);

        void AddPreQuery(Func<IQueryable<T>, IQueryable<T>> query);
    }
}