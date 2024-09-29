using ERC.Hub.Business.Common.Models;

namespace ERC.Hub.Business.Common.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException() { }

        public UnauthorizedException(ErrorResponse errorResponse) : base(errorResponse) { }
    }
}
