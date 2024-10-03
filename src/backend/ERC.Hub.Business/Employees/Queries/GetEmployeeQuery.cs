using AutoMapper;
using ERC.Hub.Business.Common.Models;
using ERC.Hub.Data.Repositories;
using MediatR;

namespace ERC.Hub.Business.Employees.Queries
{
    public record GetEmployeeQuery(int? PageNumber, int? PageSize) : IRequest<PaginatedResponse<EmployeeDto>> { }

    public class GetEmployeeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<GetEmployeeQuery, PaginatedResponse<EmployeeDto>>
    {
        public async Task<PaginatedResponse<EmployeeDto>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employees = await unitOfWork.Employee.GetAllAsync();
            if (!employees.Any())
                return new PaginatedResponse<EmployeeDto>([], request.PageNumber, request.PageSize);

            var result = mapper.Map<List<EmployeeDto>>(employees);
            return new PaginatedResponse<EmployeeDto>(result, request.PageNumber, request.PageSize);
        }
    }
}
