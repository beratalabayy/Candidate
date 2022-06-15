using Business.Helper;
using Dto.Common;
using Dto.Payments;
using RestSharp;
using Newtonsoft.Json;
using Business.Services.Transactions;
using Business.Services.Tokens;
using Microsoft.Extensions.Configuration;
using Dto.Tokens;

namespace Business.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        #region fields
        private readonly ITokenService _tokenService;
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;
        private readonly TokenRequestModel _tokenRequestModel;
        #endregion

        #region ctor
        public PaymentService(ITokenService tokenService, ITransactionService transactionService, IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _transactionService = transactionService;
            _tokenRequestModel = new TokenRequestModel();
            _tokenRequestModel.ApiKey = _configuration["TokenApiKey"].ToString();
            _tokenRequestModel.Email = _configuration["TokenEmail"].ToString();
            _tokenRequestModel.Lang = _configuration["TokenLang"].ToString();
        }
        #endregion

        #region method
        public async Task<BaseResponseModel> DoPayment(PaymentRequestModel paymentRequestModel)
        {
            paymentRequestModel.currency = "949";
            paymentRequestModel.customerId = "1";
            paymentRequestModel.description = "asdasd";
            paymentRequestModel.installmentCount = "1";
            paymentRequestModel.merchantId = 1894;
            paymentRequestModel.orderId = Guid.NewGuid().ToString();
            paymentRequestModel.txnType = "Auth";
            paymentRequestModel.userCode = "murat.karayilan@dotto.com.tr";
            paymentRequestModel.totalAmount = "1";
            paymentRequestModel.hash = null;
            paymentRequestModel.rnd = DateTime.Now.Ticks.ToString();
            paymentRequestModel.okUrl = "https://localhost:7030/api/PaymentResult/Success";
            paymentRequestModel.failUrl = "https://localhost:7030/api/PaymentResult/Failed";
            paymentRequestModel.cardNumber = "5101385101385104";
            paymentRequestModel.cvv = "000";
            paymentRequestModel.expiryDateMonth = "12";
            paymentRequestModel.expiryDateYear = "22";
            paymentRequestModel.insertCard = false;
            paymentRequestModel.use3D = true;

            var response = new BaseResponseModel();
            var tokenResponse = await _tokenService.GetToken(_tokenRequestModel);

            if (!tokenResponse.fail)
            {
                paymentRequestModel.hash = HashHelper.PaymentHash(paymentRequestModel);
                var client = new RestClient();
                var uri = new Uri("https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/Payment3d");
                var request = new RestRequest(uri, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "bearer " + tokenResponse.result.token);
                var body = JsonConvert.SerializeObject(paymentRequestModel);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                var paymentResult = client.Execute(request);
                if (paymentResult.IsSuccessful)
                {
                    await _transactionService.InsertTransactionAsync(new Dto.Transactions.TransactionModel()
                    {
                        Amount = Convert.ToDecimal(paymentRequestModel.totalAmount),
                        CardPan = paymentRequestModel.cardNumber,
                        CustomerId = paymentRequestModel.customerId,
                        OrderId = paymentRequestModel.orderId,
                        ResponseCode = paymentResult.StatusCode.ToString(),
                        ResponseMessage = paymentResult.ResponseStatus.ToString(),
                        TypeId = Core.Enum.TransactionTypeEnum.Sale
                    });
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Ödeme sırasında sorun oluştu: " + paymentResult.StatusDescription;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = tokenResponse.responseMessage;
            }
            return response;

        }

        public async Task<BaseResponseModel> Success()
        {
            //callbackden dönen
            return new BaseResponseModel();
        }

        public async Task<BaseResponseModel> Failed()
        {
            return new BaseResponseModel();
        }

        #endregion

    }
}
