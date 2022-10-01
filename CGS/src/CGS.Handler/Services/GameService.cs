using CGS.Domain.Entities;
using CGS.Handler.Services.Interface;
using CGS.Infra.Provider.Interfaces;
using CGS.Utils.Enums;

namespace CGS.Handler.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly ICacheProvider<GameInfo> _cacheGameInfo;
        private readonly ICacheProvider<string> _cacheGamePGN;
        public GameService(ILogger<GameService> _logger, ICacheProvider<GameInfo> _cacheGameInfo, ICacheProvider<string> _cacheGamePGN)
        {
            this._logger = _logger;
            this._cacheGameInfo = _cacheGameInfo;
            this._cacheGamePGN = _cacheGamePGN;
        }
        public async Task<string> ConnectPlayer(string gameId, string userId, string socketId)
        {
            var gi = await _cacheGameInfo.GetAsync(RedisDBEnum.GameInfo, gameId);

            var user = gi.Players.Where(player => player.Id == userId).FirstOrDefault();

            var adv = gi.Players.Where(player => player.Id != userId).ToList();

            if (user != null)
            {
                if (!user.IsConnected)
                {
                    user.IsConnected = true;
                    user.SocketId = socketId;

                    adv.Add(user);
                    gi.Players = adv;

                    if (gi.GameStatus == GameStatus.AFK)
                        gi.GameStatus = GameStatus.HAFK;

                    if (gi.GameStatus == GameStatus.HAFK)
                        gi.GameStatus = GameStatus.Ok;
                }
                else
                {
                    var errormsg = "[Connection Error] Player is already connected";
                    _logger.LogWarning(errormsg);
                    return errormsg;
                }
            }

            else
            {
                var errormsg = "[Connection Error] Player not found.";
                _logger.LogWarning(errormsg);
                return errormsg;
            }

            await _cacheGameInfo.SetAsync(RedisDBEnum.GameInfo, gameId, gi);
            var str = $"User {userId} is connected to the game {gameId} as player";

            _logger.LogInformation(str);
            return str;
        }

        public async Task<bool> CreateGame(string gameId, string wPlayerId, string bPlayerId, string initialPGN = null)
        {
            try
            {
                await _cacheGameInfo.SetAsync(RedisDBEnum.GameInfo, gameId, new GameInfo
                {
                    Id = gameId,
                    Players = new List<UserInfo> {
                    new UserInfo { Id = wPlayerId, IsPlayer = true, IsWhite = true, IsConnected = false },
                    new UserInfo { Id = bPlayerId, IsPlayer = true, IsWhite = false, IsConnected = false }
                },
                    GameStatus = GameStatus.AFK,
                    PGN = initialPGN,

                });
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<string> Move(string gameId, string userId, string moveStmt)
        {
            // Validate movement

            // persist movement

            var gi = await _cacheGameInfo.GetAsync(RedisDBEnum.GameInfo, gameId);

            if (gi == null)
                return "Game not found";

            var player = gi.Players.Where(player => player.Id == userId).FirstOrDefault();

            if (player != null)
            {
                if (gi.WhiteToPlay && player.IsWhite || !gi.WhiteToPlay && !player.IsWhite)
                {
                    var pgn = await _cacheGamePGN.GetAsync(RedisDBEnum.PGN, gameId);

                    await _cacheGamePGN.SetAsync(RedisDBEnum.PGN, gameId, moveStmt);
                }
                else 
                {
                    return "Forbidden movement";
                }
            }
            else 
            {
                return "Player not found";
            }

            return moveStmt;
        }
    }
}
