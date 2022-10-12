using StackExchange.Redis;

namespace CGS.Handler.Config
{
    public static class Cache
    {
        public static WebApplicationBuilder ConfigureCache(this WebApplicationBuilder builder, string connectionString)
        {
            var multiplexer = ConnectionMultiplexer.Connect(connectionString);
            builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            return builder;
        }
    }
}
