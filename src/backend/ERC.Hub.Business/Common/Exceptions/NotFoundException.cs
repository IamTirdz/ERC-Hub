using ERC.Hub.Business.Common.Models;

namespace ERC.Hub.Business.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() { }

        public NotFoundException(ErrorResponse errorResponse) : base(errorResponse) { }
    }
}
