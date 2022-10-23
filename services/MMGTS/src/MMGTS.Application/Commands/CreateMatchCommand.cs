using MediatR;

namespace MMGTS.Server.Commands
{
    public class CreateMatchCommand : IRequest<string>
    {
        public string WhitePlayerId { get; set; }
        public string BlackPlayerId { get; set; }
        public string TimeControl { get; set; }
    }
}
