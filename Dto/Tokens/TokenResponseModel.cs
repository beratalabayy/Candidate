
namespace Dto.Tokens
{
    public class TokenResponseModel
    {
        public bool fail { get; set; }
        public int statusCode { get; set; }
        public Result result { get; set; }
        public int count { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
    }

    public class Result
    {
        public int userId { get; set; }
        public string token { get; set; }
    }
}
