using CGS.Handler.Middlewares;

namespace CGS.Handler.Config
{
    public static class MiddlewareConfig
    {
        public static void AddMidlewares(this WebApplication app)
        {
            app.UseMiddleware<CGSHMiddleware>();
        }
    }
}
