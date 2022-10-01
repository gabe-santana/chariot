using CGS.Handler.Services.Interface;
using CGS.SharedKernel.ResponseObjects;
using CGS.Utils.Enums;

namespace CGS.Handler.Services
{
    public class WatchService : IWatchService
    {
        private readonly ILogger<WatchService> _logger;

        public WatchService(ILogger<WatchService> _logger)
        {
            this._logger = _logger;
        }

        public MessageResponseObject ConnectSpectator(string gameId, string userId)
        {
            var message = $"User {userId} is connected to the game {gameId} as spectator";

            _logger.LogInformation(message);

            return new MessageResponseObject { MessageType = MessageTypeEnum.BroadCast, Message=message };
        }
    }
}
