namespace DotNetTraining.Requests
{
    public class SortingFilter
    {
        public string SortBy { get; set; } = "Id"; // Thuộc tính mặc định để sắp xếp
        public bool Descending { get; set; } = false; // Hướng sắp xếp mặc định
    }
}
