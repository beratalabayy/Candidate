using DataAccess.Abstract.Transactions;
using Dto.Common;
using Dto.Transactions;
using Candidate.Business.ModelExtension.Transactions;

namespace Business.Services.Transactions
{
    public class TransactionService : ITransactionService
    {

        #region fields
        private readonly ITransactionDal _transactionDal;
        #endregion

        #region ctor

        public TransactionService(ITransactionDal transactionDal)
        {
            _transactionDal = transactionDal;
        }

        #endregion

        #region method
        public async Task<BaseResponseModel> InsertTransactionAsync(TransactionModel transactionModel)
        {
            var response = new BaseResponseModel();
            if (transactionModel == null)
            {
                response.IsSuccess = false;
                response.Message = "İşlem boş olamaz!";
            }
            else
            {
                var transactionEntity = transactionModel.ToTransactionEntity();
                await _transactionDal.InsertAsync(transactionEntity);
                response.IsSuccess = await _transactionDal.SaveChangesAsync();
            }
            return response;
        }
        #endregion
    }
}
