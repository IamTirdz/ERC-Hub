namespace ERC.Hub.Data.Models
{
    public class EntityBase
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
