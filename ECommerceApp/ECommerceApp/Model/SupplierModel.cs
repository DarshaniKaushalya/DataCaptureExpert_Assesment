using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Model
{
    public class SupplierModel
    {
        [Key]
        public Guid SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime? CreatedOn { get; set;}
        public bool IsActive {  get; set; }

    }
}
