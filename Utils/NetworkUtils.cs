using System.Net.Sockets;

namespace Net.Teirlinck.Utils
{
    public static class NetworkUtils
    {
        public static bool IsProcessListening(string host, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient(host, port))
                {
                    bool connStatus = client.Connected;

                    if (connStatus)
                    {
                        client.GetStream().Close();
                        client.Close();
                    }

                    return connStatus;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
