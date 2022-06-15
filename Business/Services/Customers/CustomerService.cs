using DataAccess.Abstract.Customers;
using Dto.Common;
using Dto.Customers;
using Candidate.Business.ModelExtension.Customers;
using static KPS.KPSPublicSoapClient;

namespace Business.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        #region fields

        private readonly ICustomerDal _customerDal;

        #endregion


        #region ctor

        public CustomerService(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        #endregion


        #region method

        public async Task<BaseResponseModel> InsertCustomerAsync(CustomerModel customerModel)
        {
            var response = new BaseResponseModel();
            if (customerModel == null)
            {
                response.IsSuccess = false;
            }
            else
            {
                var customerEntity = customerModel.ToCustomerEntity();
                var customerControl = await _customerDal.GetListAsync(a => a.IdentityNo == customerModel.IdentityNo);
                if (customerControl.Any())
                {
                    response.IsSuccess = false;
                    response.Message = "Müşteri daha önce kayıt edilmiştir.";
                }
                else
                {
                    customerEntity.IdentityNoVerified = false;
                    await _customerDal.InsertAsync(customerEntity);
                    if (await _customerDal.SaveChangesAsync())
                    {
                        KPS.KPSPublicSoapClient client = new KPS.KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);
                        var kpsResult = await client.TCKimlikNoDogrulaAsync(customerEntity.IdentityNo, customerEntity.Name, customerEntity.Surname, customerEntity.BirhDate.Year);
                        response.IsSuccess = kpsResult.Body.TCKimlikNoDogrulaResult;
                        if (response.IsSuccess)
                        {
                            customerEntity.IdentityNoVerified = true;
                            _customerDal.Update(customerEntity);
                            if (!await _customerDal.SaveChangesAsync())
                            {
                                response.IsSuccess = false;
                                response.Message = "Beklenmeyen bir hata";
                            }
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Kayıt başarı fakat doğrulama olmadı!";
                        }
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Beklenmeyen bir hata";
                    }
                }
            }
            return response;
        }

        #endregion
    }
}
