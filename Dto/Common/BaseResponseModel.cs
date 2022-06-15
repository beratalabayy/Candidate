using System;

namespace Dto.Common
{
    [Serializable]
    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public object Data { get; set; }
        public string ErrorCode { get; set; }
    }
}
