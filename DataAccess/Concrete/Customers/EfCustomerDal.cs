using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Customers;
using Entity.Customers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Categories
{
    public class EfCustomerDal : EfRepositoryBase<Customer>, ICustomerDal
    {
        public EfCustomerDal(DbContext context) : base(context)
        {

        }
    }
}
