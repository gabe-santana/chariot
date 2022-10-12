using CGS.SharedKernel.ResponseObjects;

namespace CGS.Handler.Hubs.Interface
{
    public interface ICGSHandlerHub
    {
        Task<MessageResponseObject> HandleAsync(string cmd, string socketId);
    }
}