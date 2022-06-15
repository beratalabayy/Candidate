using Core.Entites;
using Core.Enum;
using Entity.Customers;

namespace Entity.Transactions
{
    public class Transaction : BaseEntity
    {
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public TransactionTypeEnum TypeId { get; set; }
        public decimal Amount { get; set; }
        public string CardPan { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public int StatusId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
