using Business.Services.Tokens;
using Candidate.Controllers.Base;
using Dto.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Controllers
{
    public class TokenController : BaseController
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken([FromQuery]TokenRequestModel tokenModel)
        {
            var response = await _tokenService.GetToken(tokenModel);
            return Ok(response);
        }
    }
}
