using System.ComponentModel.DataAnnotations;

namespace MMGTS.Domain.Entities
{
    public class MatchData : Entity
    {
        #region PK
        [Key]
        public Guid Id { get; set; }
        #endregion

        #region Fields
        [Required]
        public string WPlayerId { get; set; }
        [Required]
        public string BPlayerId { get; set; }
        [Required]
        public string TimeControl { get; set; }
        public string? Result { get; set; } = null!;
        public string? PGN { get; set; }
        [Required]

        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime Date { get; set; }

        #endregion
    }
}
