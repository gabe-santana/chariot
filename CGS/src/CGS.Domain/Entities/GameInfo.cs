using CGS.Domain.Entities.Shared;

namespace CGS.Domain.Entities
{
    public class GameInfo : Entity
    {
        public IEnumerable<UserInfo> Players { get; set; }
        public IEnumerable<UserInfo> Spectators { get; set; }
        public bool WhiteToPlay { get; set; } = true;
        public GameStatus GameStatus { get; set; }
        public string PGN { get; set; }
        public DateTime SnapShot { get; set; }


    }

    public enum GameStatus
    {
        Ok, // Two players are playing
        HAFK, // Only one player is AFK
        AFK //Two players are AFK
    }
}
