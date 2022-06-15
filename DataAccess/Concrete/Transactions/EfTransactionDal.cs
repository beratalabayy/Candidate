using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Transactions;
using Entity.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Categories
{
    public class EfTransactionDal : EfRepositoryBase<Transaction>, ITransactionDal
    {
        public EfTransactionDal(DbContext context) : base(context)
        {

        }
    }
}
