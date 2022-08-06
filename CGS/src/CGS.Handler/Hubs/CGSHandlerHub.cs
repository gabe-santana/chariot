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

        public async Task HandleAsync(string cmd)
        {
            _logger.LogInformation($"Received {cmd}");
            switch (cmd)
            {
                case var value when value.Contains(TokenStatement.Connection):
                    var p = Parser.GetParams(cmd);
                    _connService.Connect(p[0], p[1]);
                    break;
                default:
                    break;
            }

        }
    }
}