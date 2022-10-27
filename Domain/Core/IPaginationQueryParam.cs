namespace Domain.Core
{
    public interface IPaginationQueryParam<T> : IQueryParam<T> where T : class
    {
        int Skip { get; set; }
        int Take { get; set; }
    }
}