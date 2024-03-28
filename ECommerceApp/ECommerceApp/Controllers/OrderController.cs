using ECommerceApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ECommerceApp.Interfaces;
using ECommerceApp.Repositories;

namespace ECommerceApp.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IConfiguration _configuration;

        public OrderController(IOrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

        }
        //CreateOrder by UserId
        [Route("CreateOrder/{UserId:Guid}")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromRoute] Guid UserId,[FromBody] OrderModel obj)
        {
            try
            {
                var order = await _orderRepository.CreateOrderAsync(UserId, obj);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GetActiveOrders by UserId
        [Route("GetActiveOrders/{UserId:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetActiveOrdersByCustomer([FromRoute] Guid UserId)
        {
            try
            {

                List<OrderModel> OrderList = await _orderRepository.GetActiveOrdersByCustomerAsync(UserId);
                return Ok(OrderList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
