using CGS.Handler.Services.Interface;
using System.Text;

namespace CGS.Handler.Config
{
    public static class HttpTriggerConfig
    {
        public static void MapHttpMatchDataTrigger(this WebApplication app, PathString path)
        {
            app.Map(path, async (context) =>
            {
                var service = app.Services.GetService<IGameService>();

                var gameId = context.Request.Query["gameId"];
                var wPlayerToken = context.Request.Query["wPlayerToken"];
                var bPlayerToken = context.Request.Query["bPlayerToken"];


                var gameCreateResult = await service.CreateGame(gameId: gameId, wPlayerId: wPlayerToken, bPlayerId: bPlayerToken);


                if (gameCreateResult)
                {
                    var bytes = Encoding.UTF8.GetBytes("Game create successfully.");
                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                }
                else
                {
                    var bytes = Encoding.UTF8.GetBytes("Game could not be created.");
                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                }
            });
        }
    }
}
