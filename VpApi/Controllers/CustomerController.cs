using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VpApi.DTOs;
using VpBusinessLogic.Services;
using VpDataAccess.Models;

namespace VpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerDTO customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return BadRequest("Customer data is required.");
                }

                if (string.IsNullOrEmpty(customerDto.FirstName) || string.IsNullOrEmpty(customerDto.LastName))
                {
                    return BadRequest("First name and last name are required.");
                }

                var customer = new Customer
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                };

                _customerService.AddCustomer(customer);
                return Ok("Customer successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();
                var customerDtos = customers.Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                });

                return Ok(customerDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
