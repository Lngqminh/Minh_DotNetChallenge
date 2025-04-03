namespace DotNetTraining.Requests
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1; // Số trang mặc định
        public int PageSize { get; set; } = 10;  // Kích thước trang mặc định
    }
}
