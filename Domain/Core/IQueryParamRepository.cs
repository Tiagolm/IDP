namespace Domain.Core
{
    public interface IQueryParamRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> FilterAsync(IQueryParam<T> queryParam);

        Task<PaginationResult<T>> PaginateAndFilterAsync(IPaginationQueryParam<T> queryParam);
    }
}