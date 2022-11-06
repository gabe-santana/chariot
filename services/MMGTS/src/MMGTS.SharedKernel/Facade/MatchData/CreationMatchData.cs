namespace MMGTS.SharedKernel.Facade.MatchData
{
    public record CreationMatchData
    {
        public string WPlayerId { get; set; }
        public string BPlayerId { get; set; }
        public string TimeControl { get; set; }
    }
}
