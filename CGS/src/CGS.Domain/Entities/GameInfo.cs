using CGS.Domain.Entities.Shared;

namespace CGS.Domain.Entities
{
    public class GameInfo : Entity
    {
        public IEnumerable<UserInfo> Players { get; set; }
        public IEnumerable<UserInfo> Spectators { get; set; }
        public bool WhiteToPlay { get; set; }
        public GameStatus GameStatus { get; set; }
        public string PGN { get; set; }
        public DateTime SnapShot { get; set; }


    }

    public enum GameStatus
    {
        Ok,
        WAFK,
        BAFK,
        AFK
    }
}
