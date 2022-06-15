using Dto.Tokens;
using RestSharp;
using Newtonsoft.Json;

namespace Business.Services.Tokens
{
    public class TokenService : ITokenService
    {

        #region method
        public async Task<TokenResponseModel> GetToken(TokenRequestModel tokenRequestModel)
        {
            TokenResponseModel result = new TokenResponseModel();
            var client = new RestClient();
            var uri = new Uri("https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant");
            var request = new RestRequest(uri, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(tokenRequestModel);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var tokenResult = client.Execute(request);
            if (tokenResult.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<TokenResponseModel>(tokenResult.Content);
            }
            else
            {
                result.fail = true;
                result.responseMessage = "Token Servisinde Hata oluştu:" + tokenResult.ErrorMessage;
            }
          
            return result;
        }

        #endregion
    }
}
