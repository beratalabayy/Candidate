using Core.Entites;
using Entity.Transactions;

namespace Entity.Customers
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirhDate { get; set; }
        public long IdentityNo { get; set; }
        public bool IdentityNoVerified { get; set; }
        public int StatusId { get; set; }
        public virtual IEnumerable<Transaction> Transactions { get; set; }
    }
}
