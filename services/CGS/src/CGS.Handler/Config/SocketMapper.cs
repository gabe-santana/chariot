using CGS.Handler.Middlewares;

namespace CGS.Handler.Config
{
    public static class SocketMapper
    {
        public static void MapSockets(this WebApplication app, PathString path)
        {
            app.Map(path, (x) => x.UseMiddleware<CGSHMiddleware>());
        }
    }
}
