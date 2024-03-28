using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Model
{
    public class CustomerModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
