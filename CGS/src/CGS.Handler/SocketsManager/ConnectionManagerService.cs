using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace CGS.Handler.SocketsManager
{
    public class ConnectionManagerService
    {

        private ConcurrentDictionary<string, WebSocket> Connections = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketsById(string id)
        {
            return this.Connections.FirstOrDefault(conn => conn.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllConnections()
        {
            return this.Connections;
        }

        public string GetSocketId(WebSocket socket)
        {
            return Connections.FirstOrDefault(conn => conn.Value == socket).Key;
        }

        public async Task RemoveSocketAsync(string id)
        {
            Connections.TryRemove(id, out var socket);
            if (socket != null)
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
        }

        public bool AddSocket(WebSocket socket)
        {
            return Connections.TryAdd(SetConnectionId(), socket);
        }


        private string SetConnectionId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
