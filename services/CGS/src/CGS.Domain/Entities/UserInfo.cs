using CGS.Domain.Entities.Shared;


namespace CGS.Domain.Entities
{
    public class UserInfo : Entity
    {
        public bool IsPlayer { get; set; }
        public bool IsWhite { get; set; }
        public bool IsConnected { get; set; }
        public string? SocketId { get; set; }
    }
}
