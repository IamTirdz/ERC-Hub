namespace ERC.Hub.Business.Common.Models
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(string message, int? errorCode = null)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public int? ErrorCode { get; set; }
        public string Message { get; set; } = null!;
        public IDictionary<string, string[]> Errors { get; set; } = null!;
    }
}
