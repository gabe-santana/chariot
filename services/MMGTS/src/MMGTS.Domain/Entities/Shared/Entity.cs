namespace MMGTS.Domain.Entities
{
    public class Entity
    {
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; } = null!;
    }
}
