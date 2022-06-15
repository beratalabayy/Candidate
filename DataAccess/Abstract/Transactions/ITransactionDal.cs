using Core.DataAccess;
using Entity.Transactions;
namespace DataAccess.Abstract.Transactions
{
    public interface ITransactionDal : IEntityRepository<Transaction>
    {

    }
}
