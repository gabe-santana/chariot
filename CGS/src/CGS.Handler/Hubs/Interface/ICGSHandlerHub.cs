namespace CGS.Handler.Hubs.Interface
{
    public interface ICGSHandlerHub
    {
        Task<string> HandleAsync(string cmd);
    }
}