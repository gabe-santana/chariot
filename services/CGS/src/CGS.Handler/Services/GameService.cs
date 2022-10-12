using CGS.Domain.Entities;
using CGS.Handler.Services.Interface;
using CGS.Infra.Provider.Interfaces;
using CGS.SharedKernel.ResponseObjects;
using CGS.Utils.Enums;

namespace CGS.Handler.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly ICacheProvider<GameInfo> _cacheGameInfo;
        private readonly ICacheProvider<string> _cacheGameMv;
        public GameService(ILogger<GameService> _logger, ICacheProvider<GameInfo> _cacheGameInfo, ICacheProvider<string> _cacheGamePGN)
        {
            this._logger = _logger;
            this._cacheGameInfo = _cacheGameInfo;
            this._cacheGameMv = _cacheGamePGN;
        }
        public async Task<MessageResponseObject> ConnectPlayer(string gameId, string userId, string socketId)
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
                    return new MessageResponseObject { MessageType = MessageTypeEnum.Error, Message = errormsg };
                }
            }

            else
            {
                var errormsg = "[Connection Error] Player not found.";
                _logger.LogWarning(errormsg);
                return new MessageResponseObject { MessageType = MessageTypeEnum.Error, Message = errormsg }; ;
            }

            await _cacheGameInfo.SetAsync(RedisDBEnum.GameInfo, gameId, gi);
            var sucessMessage = $"User {userId} is connected to the game {gameId} as player";

            _logger.LogInformation(sucessMessage);
            return new MessageResponseObject { MessageType = MessageTypeEnum.BroadCast, Message = sucessMessage }; ;
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
                    Moves = initialPGN,

                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<MessageResponseObject> Move(string gameId, string userId, string moveStmt)
        {
            // Validate movement

            // persist movement

            var gi = await _cacheGameInfo.GetAsync(RedisDBEnum.GameInfo, gameId);

            if (gi == null)
            {
                var errorMessage = "Game not found";
                _logger.LogInformation(errorMessage);
                return new MessageResponseObject { MessageType = MessageTypeEnum.Error, Message = errorMessage };
            }


            var player = gi.Players.Where(player => player.Id == userId).FirstOrDefault();

            if (player != null)
            {
                if (gi.WhiteToPlay == player.IsWhite || !gi.WhiteToPlay && !player.IsWhite)
                {
                    var mvList = await _cacheGameMv.GetAsync(RedisDBEnum.MvDB, gameId);

                    await _cacheGameMv.SetAsync(RedisDBEnum.MvDB, gameId, String.IsNullOrEmpty(mvList) ? moveStmt : $"{mvList},{moveStmt}");

                    gi.WhiteToPlay = !gi.WhiteToPlay;
                    await _cacheGameInfo.SetAsync(RedisDBEnum.GameInfo, gameId, gi);
                }
                else
                {
                    var errorMessage = "Forbidden movement. Reason: It's not your time!";
                    return new MessageResponseObject { MessageType = MessageTypeEnum.Error, Message = errorMessage };
                }
            }
            else
            {
                var errorMessage = "Player not found";
                return new MessageResponseObject { MessageType = MessageTypeEnum.Error, Message = errorMessage };
            }

            return new MessageResponseObject { MessageType = MessageTypeEnum.BroadCast, Message = moveStmt };
        }
    }
}
