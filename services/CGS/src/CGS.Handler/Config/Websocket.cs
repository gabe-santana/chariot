namespace CGS.Config
{
    public static class WebSocketConfig
    {
        public static void AddWebSocket(this WebApplication app)
        {
            var wsOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };

            app.UseWebSockets(wsOptions);
        }
    }
}
