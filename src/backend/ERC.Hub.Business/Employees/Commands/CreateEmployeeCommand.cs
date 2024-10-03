using ERC.Hub.Business.Common.Exceptions;
using ERC.Hub.Business.Common.Models;
using ERC.Hub.Data.Repositories;
using FluentValidation;
using MediatR;

namespace ERC.Hub.Business.Employees.Commands
{
    public record CreateEmployeeCommand(CreateEmployeeDto Employee) : IRequest<long> { }

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(v => v.Employee.FirstName).NotEmpty();
            RuleFor(v => v.Employee.LastName).NotEmpty();
            RuleFor(v => v.Employee.DateOfBirth).NotNull();
            RuleFor(v => v.Employee.Gender).NotEmpty();
            RuleFor(v => v.Employee.Address).NotEmpty();
            RuleFor(v => v.Employee.Email).NotEmpty()
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(v => v.Employee.PositionId).NotEqual(0);
            RuleFor(v => v.Employee.EmploymentType).NotNull();
            RuleFor(v => v.Employee.DepartmentId).NotEqual(0);
        }
    }

    public class CreateEmployeeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateEmployeeCommand, long>
    {
        public async Task<long> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork.Employee.GetByEmailAsync(request.Employee.Email);
            if (employee != null)
                throw new BadRequestException(new ErrorResponse { Message = "Employee already exist" });

            var newEmployee = new Data.Models.Employee
            {
                FirstName = request.Employee.FirstName,
                LastName = request.Employee.LastName,
                DateOfBirth = request.Employee.DateOfBirth,
                Gender = request.Employee.Gender,
                Address = request.Employee.Address,
                Email = request.Employee.Email,
                ContactNumber = request.Employee.ContactNumber,
                PositionId = request.Employee.PositionId,
                EmploymentType = (long)request.Employee.EmploymentType,
                DepartmentId = request.Employee.DepartmentId,
                ManagerId = request.Employee.ManagerId,
                HiredDate = request.Employee.HiredDate,
            };

            unitOfWork.Employee.Add(newEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return newEmployee.Id;
        }
    }
}
