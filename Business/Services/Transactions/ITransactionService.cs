using Dto.Common;
using Dto.Transactions;

namespace Business.Services.Transactions
{
    public interface ITransactionService
    {
        Task<BaseResponseModel> InsertTransactionAsync(TransactionModel transactionModel);
    }
}
