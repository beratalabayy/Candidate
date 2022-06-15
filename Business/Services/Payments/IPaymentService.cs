using Dto.Common;
using Dto.Payments;
using System.Threading.Tasks;

namespace Business.Services.Payments
{
    public interface IPaymentService
    {
        Task<BaseResponseModel> DoPayment(PaymentRequestModel paymentRequestModel);
        Task<BaseResponseModel> Success();
        Task<BaseResponseModel> Failed();
    }
}
