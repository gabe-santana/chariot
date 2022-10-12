using CGS.SharedKernel.ResponseObjects;

namespace CGS.Handler.Services.Interface
{
    public interface IWatchService
    {
        MessageResponseObject ConnectSpectator(string gameId, string userId);
    }
}
