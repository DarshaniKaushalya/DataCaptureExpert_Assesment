using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Model
{
    public class ProductModel
    {
        [Key]
        public Guid ProductId  { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid SupplierId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set;}
        public SupplierModel Supplier { get; set; }

    }
}
