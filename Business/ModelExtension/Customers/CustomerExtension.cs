using Dto.Customers;
using Entity.Customers;

namespace Candidate.Business.ModelExtension.Customers
{
    public static class TransactionExtension
    {
        public static CustomerModel ToCustomerModel(this Customer customer)
        {
            var customerModel = new CustomerModel
            {
                BirhDate = customer.BirhDate,
                IdentityNo = customer.IdentityNo,
                Name = customer.Name,
                Surname = customer.Surname,
            };
            return customerModel;
        }

        public static Customer ToCustomerEntity(this CustomerModel customerModel)
        {
            return new Customer
            {
                BirhDate = customerModel.BirhDate,
                IdentityNo = customerModel.IdentityNo,
                Name = customerModel.Name,
                Surname = customerModel.Surname,
            };
        }
    }
}
