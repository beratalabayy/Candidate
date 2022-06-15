
using Dto.Payments;
using System.Security.Cryptography;
using System.Text;

namespace Business.Helper
{
    public static class HashHelper
    {
        public static string PaymentHash(PaymentRequestModel request)
        {
            var hashString = $"{request.hash}{request.userCode}{request.rnd}{request.txnType}{request.totalAmount}{request.customerId}{request.orderId}{request.okUrl}{request.failUrl}";
            var s512 = SHA512.Create();
            var byteConverter = new UnicodeEncoding();
            var bytes = s512.ComputeHash(byteConverter.GetBytes(hashString));
            var hash = BitConverter.ToString(bytes).Replace("-", "");
            return hash;
        }
    }
}
