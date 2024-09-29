using ERC.Hub.Business.Common.Models;

namespace ERC.Hub.Business.Common.Exceptions
{
    public class InputValidationException : BaseException
    {
        public InputValidationException() { }

        public InputValidationException(ErrorResponse errorResponse) : base(errorResponse) { }
    }
}
