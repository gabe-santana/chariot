using CGS.Handler.Hubs;
using CGS.Handler.Hubs.Interface;
using CGS.Handler.Services;
using CGS.Handler.Services.Interface;
using CGS.Handler.SocketsManager;
using CGS.Infra.Provider;
using CGS.Infra.Provider.Interfaces;
using System.Reflection;

namespace CGS.Handler.Config
{
    public static class IoC
    {
        public static WebApplicationBuilder ConfigureIoC(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IWatchService, WatchService>();
            builder.Services.AddSingleton<IGameService, GameService>();

            builder.Services.AddSingleton<ICGSHandlerHub, CGSHandlerHub>();

            builder.Services.AddTransient<ConnectionManagerService>();

            builder.Services.AddSingleton(typeof(ICacheProvider<>), typeof(CacheProvider<>));

            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketManagerService))
                    builder.Services.AddSingleton(type);
            }

            return builder;
        }
    }
}
