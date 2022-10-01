using CGS.SharedKernel.ResponseObjects;

namespace CGS.Handler.Services.Interface
{
    public interface IGameService
    {
        Task<MessageResponseObject> ConnectPlayer(string gameId, string userId, string socketId);

        Task<MessageResponseObject> Move(string gameId, string userId, string moveStmt);

        Task<bool> CreateGame(string gameId, string wPlayerId, string bPlayerId, string initialPGN = null);
    }
}
