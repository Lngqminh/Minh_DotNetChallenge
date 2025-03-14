using Common.Domains.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetTraining.Domains.Entities
{
    [Table("Product")]
    public class Product : SystemLogEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
