namespace MMGTS.Domain.Entities
{
    public class MatchData : Entity
    {
        public Guid Id { get; set; }
        public string WPlayerId { get; set; }
        public string BPlayerId { get; set; }
        public string TimeControl { get; set; }
        public string Result { get; set; }
        public string PGN { get; set; }
        public DateTime Date { get; set; }
    }
}
