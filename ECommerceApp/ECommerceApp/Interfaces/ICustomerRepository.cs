using ECommerceApp.Model;

namespace ECommerceApp.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerModel>> GetAllCustomerAsync();
        Task <CustomerModel> CreateCustomerAsync(CustomerModel obj);
        Task<CustomerModel> UpdateCustomerAsync(Guid customerId, CustomerModel obj);
        Task <bool> DeleteCustomerAsync(Guid customerId);
        Task<bool> CheckCustomerHasOrderAsync(Guid customerId);
    }
}
