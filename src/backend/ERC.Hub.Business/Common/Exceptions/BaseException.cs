using ERC.Hub.Business.Common.Models;

namespace ERC.Hub.Business.Common.Exceptions
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        public ErrorResponse ErrorResponse { get; set; } = null!;

        protected BaseException() : base(string.Empty)
        {
        }

        protected BaseException(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
