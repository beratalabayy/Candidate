using Core.Enum;

namespace Dto.Transactions
{
    public class TransactionModel 
    {
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public TransactionTypeEnum TypeId { get; set; }
        public decimal Amount { get; set; }
        public string CardPan { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public int StatusId { get; set; }
    }
}
