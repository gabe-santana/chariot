using CGS.Handler.Services.Interface;

namespace CGS.Handler.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly ILogger<ConnectionService> _logger;

        public ConnectionService(ILogger<ConnectionService> _logger)
        {
            this._logger = _logger;
        }

        public string Connect(string gameId, string userId = "")
        {
            _logger.LogInformation($"User {userId} has been conected");
            return "";
        }
    }
}
