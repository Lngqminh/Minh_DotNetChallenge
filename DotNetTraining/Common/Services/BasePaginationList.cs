namespace DotNetTraining.Common.Services
{
    public class BasePaginationList<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public BasePaginationList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = new List<T>(items);
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
