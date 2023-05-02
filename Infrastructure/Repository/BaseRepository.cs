using Domain.Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly DbSet<T> _set;
        public IQueryable<T> Query { get; set; }

        public BaseRepository(ApplicationContext applicationContext)
        {
            _set = applicationContext.Set<T>();

            if (_set == null)
                throw new InvalidOperationException($"Tipo inválido de DbSet: {typeof(T).Name}");
            
            Query = _set.AsQueryable();
        }

        public async Task<T> Add(T obj)
        {
            await _set.AddAsync(obj);
            return obj;
            
        }

        public Task Update(T obj)
        {
            _set.Update(obj);
            return Task.CompletedTask;
        }

        public async Task Delete(int id)
        {
            var obj = await _set.FirstOrDefaultAsync(x => x.Id == id);

            if (obj == null)
                throw new InvalidOperationException($"Id: {id} para remover {typeof(T).Name} é invalido");

            await Remove(obj);
        }

        public async Task<IEnumerable<T>> List()
        {
            return await Query.ToListAsync();
        }

        public Task<T> GetById(int id)
        {
            return Query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void AddPreQuery(Func<IQueryable<T>, IQueryable<T>> query)
        {
            Query = query(Query);
        }

        private Task Remove(T obj)
        {
            _set.Remove(obj);
            return Task.CompletedTask;
        }
    }
}