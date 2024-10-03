namespace ERC.Hub.Data.Models
{
    public class Department : EntityBase
    {
        public long? ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
