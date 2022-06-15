using Dto.Tokens;

namespace Business.Services.Tokens
{
    public interface ITokenService
    {
        Task<TokenResponseModel> GetToken(TokenRequestModel tokenRequestModel);
    }
}
