using Dto.Common;
using Dto.Customers;

namespace Business.Services.Customers
{
    public interface ICustomerService
    {
        Task<BaseResponseModel> InsertCustomerAsync(CustomerModel customerModel);
    }
}
