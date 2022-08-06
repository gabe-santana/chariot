namespace CGS.Handler.Hubs.Interface
{
    public interface ICGSHandlerHub
    {
        Task HandleAsync(string cmd);
    }
}