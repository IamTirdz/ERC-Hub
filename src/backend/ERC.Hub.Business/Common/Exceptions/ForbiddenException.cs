using ERC.Hub.Business.Common.Models;

namespace ERC.Hub.Business.Common.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException() { }

        public ForbiddenException(ErrorResponse errorResponse) : base(errorResponse) { }
    }
}
