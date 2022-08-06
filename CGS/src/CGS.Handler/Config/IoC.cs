using CGS.Handler.Hubs;
using CGS.Handler.Hubs.Interface;
using CGS.Handler.Services;
using CGS.Handler.Services.Interface;


namespace CGS.Handler.Config
{
    public static class IoC
    {
        public static WebApplicationBuilder ConfigureIoC(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IConnectionService, ConnectionService>();
            builder.Services.AddScoped<ICGSHandlerHub, CGSHandlerHub>();

            return builder;
        }
    }
}
