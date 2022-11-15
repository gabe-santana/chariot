using MediatR;

namespace MMGTS.Application.Notifications
{
    public class CreatedMatchNotification : INotification
    {
        public string MatchId { get; set; }
        public string WhitePlayerId { get; set; }
        public string BlackPlayerId { get; set; }
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
    }
}
