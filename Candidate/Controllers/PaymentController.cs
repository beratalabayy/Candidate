using Business.Services.Payments;
using Candidate.Controllers.Base;
using Dto.Payments;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("DoPayment")]
        public async Task<IActionResult> DoPayment([FromBody]PaymentRequestModel paymentModel)
        {
            var response = await _paymentService.DoPayment(paymentModel);
            return Ok(response);
        }
       
    }
}
