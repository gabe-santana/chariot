namespace CGS.Handler.Services.Interface
{
    public interface IConnectionService
    {
        string Connect(string userId, string gameId = "");
    }
}
