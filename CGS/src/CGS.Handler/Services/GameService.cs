using CGS.Domain.Entities;
using CGS.Handler.Services.Interface;
using CGS.Infra.Provider.Interfaces;

namespace CGS.Handler.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly ICacheProvider<GameInfo> _cache;
        public GameService(ILogger<GameService> _logger, ICacheProvider<GameInfo> _cache)
        {
            this._logger = _logger;
            this._cache = _cache;
        }
        public async Task<string> ConnectPlayer(string gameId, string userId, string socketId)
        {

            var gi = await _cache.GetAsync(0, gameId);

            var user = gi.Players.Where(player => player.Id == userId).FirstOrDefault();

            var adv = gi.Players.Where(player => player.Id != userId).ToList();

            if (user != null)
            {
                user.IsConnected = true;
                user.SocketId = socketId;

                adv.Add(user);
                gi.Players = adv;
            }

            else
            {
                var errormsg = "[Connection Error] Player not found.";
                _logger.LogWarning(errormsg);
                return errormsg;
            }

            await _cache.SetAsync(0, gameId, gi);
            var str = $"User {userId} is connected to the game {gameId} as player";

            _logger.LogInformation(str);
            return str;
        }

        public async Task<string> Move(string gameId, string userId, string moveStmt)
        {
            // Validate movement

            // persist movement

            var gi = await _cache.GetAsync(0, gameId);

            gi.PGN += moveStmt;

            await _cache.SetAsync(0, gameId, gi);

            return moveStmt;
        }


    }
}
