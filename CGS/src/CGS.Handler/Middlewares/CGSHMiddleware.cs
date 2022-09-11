using CGS.Handler.Handlers;
using System.Net.WebSockets;

namespace CGS.Handler.Middlewares
{
    public class CGSHMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CGSHMiddleware> _logger;
        private SocketHandler messageHandler { get; set; }

        public CGSHMiddleware(RequestDelegate _next, ILogger<CGSHMiddleware> _logger, SocketHandler messageHandler)
        {
            this._next = _next;
            this._logger = _logger;
            this.messageHandler = messageHandler;
        }


        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> messageHandler)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                messageHandler(result, buffer);
            }
        }


        public async Task Invoke(HttpContext context, ILogger<CGSHMiddleware> _logger)
        {
            try
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var socket = await context.WebSockets.AcceptWebSocketAsync();

                    await messageHandler.OnConnected(socket);


                    await Receive(socket, async (result, buffer) =>
                    {
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            await messageHandler.Receive(socket, result, buffer);
                        }
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await messageHandler.OnDisconnected(socket);
                        }
                    });
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    _logger.LogWarning("A non-websocket connection has been attempted");
                }

            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
            }
        }
    }
}
