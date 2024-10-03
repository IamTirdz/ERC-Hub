using ERC.Hub.Common.Enums;

namespace ERC.Hub.Business.Employees.Commands
{
    public class CreateEmployeeDto
    {
        public string? CardId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long PositionId { get; set; }
        public long? ManagerId { get; set; }
        public long DepartmentId { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public DateTime HiredDate { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string Address { get; set; } = null!;
    }
}
