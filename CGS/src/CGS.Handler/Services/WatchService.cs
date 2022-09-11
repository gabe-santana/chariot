using CGS.Handler.Services.Interface;

namespace CGS.Handler.Services
{
    public class WatchService : IWatchService
    {
        private readonly ILogger<WatchService> _logger;

        public WatchService(ILogger<WatchService> _logger)
        {
            this._logger = _logger;
        }

        public string ConnectSpectator(string gameId, string userId)
        {
            var str = $"User {userId} is connected to the game {gameId} as spectator";

            _logger.LogInformation(str);

            return str;
        }
    }
}
