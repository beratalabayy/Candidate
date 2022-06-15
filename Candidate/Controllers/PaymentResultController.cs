using Business.Services.Payments;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Controllers
{
    [Route("api/[controller]/[action]")]

    public class PaymentResultController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentResultController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost()]
        public async Task<IActionResult> Success([FromForm] IFormCollection form)
        {
            var response = await _paymentService.Success();
            return Content("");
        }

        [HttpPost()]
        public async Task<IActionResult> Failed([FromForm] IFormCollection form)
        {
            var response = await _paymentService.Failed();
            return Content("");
        }
    }
}
