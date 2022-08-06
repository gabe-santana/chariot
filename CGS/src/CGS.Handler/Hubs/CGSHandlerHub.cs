using CGS.Handler.Hubs.Interface;
using CGS.Handler.Services.Interface;
using CGS.Handler.Utils;

namespace CGS.Handler.Hubs
{
    public class CGSHandlerHub : ICGSHandlerHub
    {
        private readonly ILogger<CGSHandlerHub> _logger;
        private readonly IConnectionService _connService;

        public CGSHandlerHub(ILogger<CGSHandlerHub> _logger, IConnectionService _connService)
        {
            this._logger = _logger;
            this._connService = _connService;
        }

        public async Task<string> HandleAsync(string cmd)
        {
            _logger.LogInformation($"Received {cmd}");
            switch (cmd)
            {
                case var value when value.Contains(TokenStatement.Connect):
                    var paramsPlayer = Parser.GetParams(cmd);
                    _connService.Connect(paramsPlayer[0], paramsPlayer[1], paramsPlayer[2] == "1");
                    return "connected";

                case var value when value == "ping":
                    return "pong";
                default:
                    break;
            }

            return "";

        }
    }
}