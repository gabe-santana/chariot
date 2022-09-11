using CGS.Handler.Hubs.Interface;
using CGS.Handler.SocketsManager;
using System.Net.WebSockets;
using System.Text;

namespace CGS.Handler.Handlers
{
    public class SocketHandler : SocketManagerService
    {
        private readonly ICGSHandlerHub cgsHub;

        public SocketHandler(ICGSHandlerHub cgsHub, ConnectionManagerService ConnectionManagerService) : base(ConnectionManagerService)
        {
            this.cgsHub = cgsHub;
        }


        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketId = base.ConnectionManagerService.GetSocketId(socket);
            await SendMessageToAll($"{socketId} Connected");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = base.ConnectionManagerService.GetSocketId(socket);

            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var _result = await cgsHub.HandleAsync(message, base.ConnectionManagerService.GetSocketId(socket));
            await SendMessageToAll(_result);
        }
    }

}
