using Dto.Transactions;
using Entity.Transactions;

namespace Candidate.Business.ModelExtension.Transactions
{
    public static class TransactionExtension
    {
        public static TransactionModel ToTransactionsModel(this Transaction transaction)
        {
            var transactionModel = new TransactionModel
            {
               Amount = transaction.Amount,   
               StatusId = transaction.StatusId,    
               CardPan  = transaction.CardPan,
               CustomerId = transaction.CustomerId,   
               OrderId= transaction.OrderId,
               ResponseCode = transaction.ResponseCode,
               ResponseMessage = transaction.ResponseMessage,
               TypeId = transaction.TypeId
            };
            return transactionModel;
        }

        public static Transaction ToTransactionEntity(this TransactionModel transactionModel)
        {
            return new Transaction
            {
                Amount = transactionModel.Amount,
                StatusId = transactionModel.StatusId,
                CardPan = transactionModel.CardPan,
                CustomerId = transactionModel.CustomerId,
                OrderId = transactionModel.OrderId,
                ResponseCode = transactionModel.ResponseCode,
                ResponseMessage = transactionModel.ResponseMessage,
                TypeId = transactionModel.TypeId
            };
        }
    }
}
