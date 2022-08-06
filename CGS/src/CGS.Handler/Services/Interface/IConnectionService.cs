namespace CGS.Handler.Services.Interface
{
    public interface IConnectionService
    {
        string Connect(string gameId, string userId, bool isPlayer);
    }
}
