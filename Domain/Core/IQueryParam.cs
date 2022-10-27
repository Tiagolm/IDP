namespace Domain.Core
{
    public interface IQueryParam<T> where T : class
    {
        IQueryable<T> ApplyFilter(IQueryable<T> query);
    }
}