namespace Domain.Core
{
    public class PaginationResult<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}