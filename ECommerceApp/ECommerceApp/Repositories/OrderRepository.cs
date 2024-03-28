using ECommerceApp.Interfaces;
using ECommerceApp.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ECommerceApp.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<OrderModel> CreateOrderAsync(Guid UserId, OrderModel obj)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_CreateOrder";

                obj.OrderId = Guid.NewGuid();

                command.Parameters.AddWithValue("@OrderId", obj.OrderId);
                command.Parameters.AddWithValue("@ProductId", obj.ProductId);
                command.Parameters.AddWithValue("@OrderStatus", obj.OrderStatus);
                command.Parameters.AddWithValue("@OrderType", obj.OrderType);
                command.Parameters.AddWithValue("@OrderBy", UserId);
                command.Parameters.AddWithValue("@OrderedOn", obj.OrderedOn);
                command.Parameters.AddWithValue("@ShippedOn", obj.ShippedOn);
                command.Parameters.AddWithValue("@IsActive", obj.IsActive);

                connection.Open();
                command.ExecuteNonQuery();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderModel>> GetActiveOrdersByCustomerAsync(Guid UserId)
        {
          
                List<OrderModel> orders = new List<OrderModel>();
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetActiveOrders";

                command.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderModel order = new OrderModel
                    {
                        OrderId = reader.GetGuid(reader.GetOrdinal("OrderId")),
                        ProductId = reader.GetGuid(reader.GetOrdinal("ProductId")),
                        OrderStatus = reader.GetInt32(reader.GetOrdinal("OrderStatus")),
                        OrderType = reader.GetInt32(reader.GetOrdinal("OrderType")),
                        OrderBy = reader.GetGuid(reader.GetOrdinal("OrderBy")),
                        OrderedOn = reader.GetDateTime(reader.GetOrdinal("OrderedOn")),
                        ShippedOn = reader.GetDateTime(reader.GetOrdinal("ShippedOn")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),

                        Product = new ProductModel
                        {
                            ProductId = reader.GetGuid(reader.GetOrdinal("ProductId")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            SupplierId = reader.GetGuid(reader.GetOrdinal("SupplierId")),
                            CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),

                            Supplier = new SupplierModel
                            {
                                SupplierId = reader.GetGuid(reader.GetOrdinal("SupplierId")),
                                SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            }
                        },
                    };
                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
