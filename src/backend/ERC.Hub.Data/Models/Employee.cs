namespace ERC.Hub.Data.Models
{
    public class Employee : EntityBase
    {
        public string? CardId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public long PositionId { get; set; }
        public virtual Position? Position { get; set; }
        
        public long? ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }

        public long DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public long EmploymentType { get; set; }
        public DateTime HiredDate { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string Address { get; set; } = null!;
    }
}
