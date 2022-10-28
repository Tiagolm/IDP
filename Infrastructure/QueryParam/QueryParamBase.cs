using Domain.Core;

namespace Infrastructure.QueryParam
{
    public abstract class QueryParamBase<T> : IQueryParam<T> where T : Entity
    {
        private List<Func<IQueryable<T>, IQueryable<T>>> _queries = new List<Func<IQueryable<T>, IQueryable<T>>>();

        public IQueryable<T> ApplyFilter(IQueryable<T> query)
        {
            Filter();
            foreach (var funcQuery in _queries)
                query = funcQuery(query);

            return query;
        }

        protected void AddQuery(Func<IQueryable<T>, IQueryable<T>> query)
        {
            _queries.Add(query);
        }
        public abstract void Filter();
    }
}
