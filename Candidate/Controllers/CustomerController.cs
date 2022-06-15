using Business.Services.Customers;
using Candidate.Controllers.Base;
using Dto.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("InsertCustomer")]
        public async Task<IActionResult> InsertCustomer([FromBody] CustomerModel customerModel)
        {
            var response = await _customerService.InsertCustomerAsync(customerModel);
            return Ok(response);
        }
    }
}
