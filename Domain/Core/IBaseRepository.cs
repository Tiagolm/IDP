namespace Domain.Core
{
    public interface IBaseRepository<T> where T : Entity
    {
        IQueryable<T> Query { get; }

        Task<IEnumerable<T>> List();

        Task<T> GetById(int id);

        Task Add(T contato);

        Task Update(T contato);

        Task Delete(int id);

        void AddPreQuery(Func<IQueryable<T>, IQueryable<T>> query);
    }
}