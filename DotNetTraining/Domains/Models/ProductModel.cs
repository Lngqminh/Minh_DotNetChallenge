namespace DotNetTraining.Domains.Models
{
    public class ProductModel
    {
        public Guid? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

    }
}
