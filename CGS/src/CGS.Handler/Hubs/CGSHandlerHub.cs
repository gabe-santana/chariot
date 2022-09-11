using CGS.Handler.Hubs.Interface;
using CGS.Handler.Services.Interface;
using CGS.Handler.Utils;

namespace CGS.Handler.Hubs
{
    public class CGSHandlerHub : ICGSHandlerHub
    {
        private readonly ILogger<CGSHandlerHub> _logger;
        private readonly IWatchService _watchService;
        private readonly IGameService _gameService;


        public CGSHandlerHub(ILogger<CGSHandlerHub> _logger, IWatchService _watchService, IGameService gameService)
        {
            this._logger = _logger;
            this._watchService = _watchService;
            this._gameService = gameService;
        }

        public async Task<string> HandleAsync(string cmd, string socketId)
        {
            _logger.LogInformation($"Command Received {cmd}");
            switch (cmd)
            {
                case var value when value.Contains(TokenStatement.Watch):
                    var paramsSpectator = Parser.GetParams(cmd);
                    return _watchService.ConnectSpectator(paramsSpectator[0], paramsSpectator[1]);

                case var value when value.Contains(TokenStatement.PlayGame):
                    var paramsPlayer = Parser.GetParams(cmd);
                    return _gameService.ConnectPlayer(paramsPlayer[0], paramsPlayer[1], socketId);


                case var value when value.Contains(TokenStatement.Move):
                    var paramsGame = Parser.GetParams(cmd);
                    return _gameService.Move(moveStmt: paramsGame[0], pFlag: paramsGame[1]);

                default:
                    break;
            }

            return "";

        }
    }
}