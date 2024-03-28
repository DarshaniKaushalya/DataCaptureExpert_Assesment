using ECommerceApp.Interfaces;
using ECommerceApp.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Get All Customer
        public async Task<List<CustomerModel>> GetAllCustomerAsync()
        {
                List<CustomerModel> customerList = new List<CustomerModel>();

                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllCustomer";
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                DataTable dtCustomer = new DataTable();

                connection.Open();
                sqlAdapter.Fill(dtCustomer);
                connection.Close();


                for (int i = 0; i < dtCustomer.Rows.Count; i++)
                {
                    CustomerModel customerModel = new CustomerModel();
                    customerModel.UserId = Guid.Parse(dtCustomer.Rows[i]["UserId"].ToString());
                    customerModel.Username = dtCustomer.Rows[i]["UserName"].ToString();
                    customerModel.Email = dtCustomer.Rows[i]["Email"].ToString();
                    customerModel.FirstName = dtCustomer.Rows[i]["FirstName"].ToString();
                    customerModel.LastName = dtCustomer.Rows[i]["LastName"].ToString();
                    customerModel.CreatedOn = Convert.ToDateTime(dtCustomer.Rows[i]["CreatedOn"].ToString());
                    customerModel.IsActive = Convert.ToBoolean(dtCustomer.Rows[i]["IsActive"].ToString());

                    customerList.Add(customerModel);
                
                }
          return customerList;
        }

        //Create Customer
        public async Task<CustomerModel> CreateCustomerAsync(CustomerModel obj)
        {

                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_CreateCustomer";

                obj.UserId = Guid.NewGuid();

                command.Parameters.AddWithValue("@UserId", obj.UserId);
                command.Parameters.AddWithValue("@Username", obj.Username);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                command.Parameters.AddWithValue("@LastName", obj.LastName);
                command.Parameters.AddWithValue("@CreatedOn", obj.CreatedOn);
                command.Parameters.AddWithValue("@IsActive", obj.IsActive);

                connection.Open();
                command.ExecuteNonQuery();
                return obj;

        }

        //Update Customer
        public async Task<CustomerModel> UpdateCustomerAsync(Guid customerId, CustomerModel obj)
        {
          
                if (!CustomerExists(customerId))
                {
                   return null;
                }

                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_UpdateCustomer";

                command.Parameters.AddWithValue("@UserId", customerId);
                command.Parameters.AddWithValue("@Username", obj.Username);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@FirstName", obj.FirstName);
                command.Parameters.AddWithValue("@LastName", obj.LastName);
                command.Parameters.AddWithValue("@CreatedOn", obj.CreatedOn);
                command.Parameters.AddWithValue("@IsActive", obj.IsActive);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return obj;

        }

        //Check customer exsists - For Update customer
        private bool CustomerExists(Guid customerId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Check_Customer_Exsists";

                command.Parameters.AddWithValue("@UserId", customerId);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete Customer
        public async Task<bool> DeleteCustomerAsync(Guid customerId)
        {
            try
            {
                bool customerHasOrders = await CheckCustomerHasOrderAsync(customerId);

                if (customerHasOrders)
                {
                    return false;
                }
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Delete_ECommerceDb_Customer";
                command.Parameters.AddWithValue("@UserId", customerId);

                connection.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Check Customer Has any Order Before delete the customer
        public async Task<bool> CheckCustomerHasOrderAsync(Guid customerId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECommerceAppConnectionString"));
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Check_Customer_Has_Orders";
                command.Parameters.AddWithValue("@UserId", customerId);

                await connection.OpenAsync();
                int orderCount = (int)await command.ExecuteScalarAsync();

                return orderCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
 
        }
    }
}
