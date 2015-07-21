using System.Net.Sockets;

namespace Net.Teirlinck.Utils
{
    public static class NetworkUtils
    {
        public static bool IsProcessListening(string host, int port)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Connect(host, port);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
