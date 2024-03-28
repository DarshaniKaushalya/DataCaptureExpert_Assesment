using ECommerceApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderModel> CreateOrderAsync(Guid UserId, OrderModel obj);
        Task<List<OrderModel>> GetActiveOrdersByCustomerAsync(Guid UserId);
    }
}
