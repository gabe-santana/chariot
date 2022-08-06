using CGS.Handler.Hubs.Interface;
using System.Text;

namespace CGS.Handler.Middlewares
{
    public class CGSHMiddleware
    {
        private readonly RequestDelegate _next;
        public CGSHMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task Invoke(HttpContext context, ICGSHandlerHub _cgsh, ILogger<CGSHMiddleware> _logger)
        {
            try
            {

                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var buffer = new byte[1024 * 4];
                        var receiveResult = await webSocket.ReceiveAsync(
                            new ArraySegment<byte>(buffer), CancellationToken.None);

                        do
                        {
                            var message = Encoding.Default.GetString(buffer, 0, receiveResult.Count);

                            var cgshr = await _cgsh.HandleAsync(message);

 
                            await webSocket.SendAsync(
                                new ArraySegment<byte>(Encoding.ASCII.GetBytes(cgshr), 0, cgshr.Length),
                                receiveResult.MessageType,
                                receiveResult.EndOfMessage,
                                CancellationToken.None);

                            receiveResult = await webSocket.ReceiveAsync(
                               new ArraySegment<byte>(buffer), CancellationToken.None);

                        } while (!receiveResult.CloseStatus.HasValue);

                        await webSocket.CloseAsync(
                        receiveResult.CloseStatus.Value,
                        receiveResult.CloseStatusDescription,
                        CancellationToken.None);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    await _next(context);
                }

            }
            catch (Exception error)
            {
            }
        }
    }
}
