using ERC.Hub.Business.Common.Models;

namespace ERC.Hub.Business.Common.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() { }

        public BadRequestException(ErrorResponse errorResponse) : base(errorResponse) { }
    }
}
