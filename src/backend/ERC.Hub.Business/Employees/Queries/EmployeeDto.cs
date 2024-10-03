using ERC.Hub.Business.Departments.Queries;

namespace ERC.Hub.Business.Employees.Queries
{
    public class EmployeeDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string Address { get; set; } = null!;
        public object Position { get; set; } = null!;        
        public DepartmentDto Department { get; set; } = null!;
        //public EmployeeDto? Manager { get; set; }
        public string EmploymentType { get; set; } = null!;
        public bool IsDeactivated { get; set; }
    }
}
