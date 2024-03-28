using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Model
{
    public class OrderModel
    {

        [Key]
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int OrderStatus { get; set; }
        public int OrderType {get; set;}
        public Guid OrderBy {  get; set; }
        public DateTime OrderedOn {  get; set; }
        public DateTime ShippedOn {  get; set; }
        public bool IsActive { get; set; }
        public ProductModel Product { get; set; }
        //public List<ProductModel> Products { get; set; }
    }
}
