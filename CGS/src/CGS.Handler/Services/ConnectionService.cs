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

        public string Connect(string gameId, string userId, bool isPlayer)
        {
            string uref = isPlayer ? "Player" : "Spectatator"; 

            _logger.LogInformation($"{uref} #{userId} has been connected to game #{gameId}");
      
            return "";
        }
    }
}
