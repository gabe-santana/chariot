using System.Net.WebSockets;
using System.Text;

namespace CGS.Handler.SocketsManager
{
    public abstract class SocketManagerService
    {
        public ConnectionManagerService ConnectionManagerService { get; set; }

        public SocketManagerService(ConnectionManagerService ConnectionManagerService)
        {
            this.ConnectionManagerService = ConnectionManagerService;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            await Task.Run(() => ConnectionManagerService.AddSocket(socket));
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await ConnectionManagerService.RemoveSocketAsync(ConnectionManagerService.GetSocketId(socket));
        }

        public async Task SendMessage(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(
                new ArraySegment<byte>(Encoding.ASCII.GetBytes(message),
                0, message.Length),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessageToAll(string message)
        {
            foreach (var conn in ConnectionManagerService.GetAllConnections())
            {
                await SendMessage(conn.Value, message);
            }
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
