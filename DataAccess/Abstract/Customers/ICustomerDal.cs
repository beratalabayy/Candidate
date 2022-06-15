using Core.DataAccess;
using Entity.Customers;

namespace DataAccess.Abstract.Customers
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {

    }
}
