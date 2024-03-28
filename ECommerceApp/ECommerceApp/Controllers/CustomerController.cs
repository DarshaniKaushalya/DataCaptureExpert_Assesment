using ECommerceApp.Interfaces;
using ECommerceApp.Model;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceApp.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        //GetAllCustomer
        [Route("GetAllCustomer")]
        [HttpGet]
        
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                List<CustomerModel> customerList = await _customerRepository.GetAllCustomerAsync();
                return Ok(customerList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //CreateCustomer
        [Route("CreateCustomer")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerModel obj)
        {
            try
            {
                var customer = await _customerRepository.CreateCustomerAsync(obj); 
                return Ok(customer);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //UpdateCustomer by customerId
        [Route("UpdateCustomer/{customerId:Guid}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid customerId,CustomerModel obj)
        {

            try
            {
                var customer = await _customerRepository.UpdateCustomerAsync(customerId, obj);
                if (customer != null)
                {             
                    return Ok(customer);
                }
                else
                {
                    return NotFound("Customer Not Found!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DeleteCustomer by customerId
        [Route("DeleteCustomer/{customerId:Guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid customerId)
        {

            try
            {
                bool isDeleted = await _customerRepository.DeleteCustomerAsync(customerId);

                if (!isDeleted)
                {
                    return Ok("Warning: Customer has orders. So Customer cannot delete.");
                }
                return Ok("Customer Deleted Successfully!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
    }
}
