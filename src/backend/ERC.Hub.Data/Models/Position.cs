namespace ERC.Hub.Data.Models
{
    public class Position : EntityBase
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
