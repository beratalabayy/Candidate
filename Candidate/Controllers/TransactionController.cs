using Business.Services.Transactions;
using Candidate.Controllers.Base;
using Dto.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("InsertTransaction")]
        public async Task<IActionResult> InsertTransaction([FromBody] TransactionModel transactionModel)
        {
            var response = await _transactionService.InsertTransactionAsync(transactionModel);
            return Ok(response);
        }
    }
}
