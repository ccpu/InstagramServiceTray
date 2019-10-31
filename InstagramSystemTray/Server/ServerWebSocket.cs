using System;
using System.Collections.Generic;
using System.Linq;
using Fleck;
using InstagramSystemTray.Helper;

namespace InstagramSystemTray.Server
{
    internal static class ServerWebSocket
    {
        private static readonly List<IWebSocketConnection> _allSockets = new List<IWebSocketConnection>();

        public static bool Start(int port)
        {
            _allSockets.Clear();

            try
            {
                var server = new WebSocketServer("ws://0.0.0.0:" + port);
                server.Start(socket =>
                {
                    socket.OnOpen = () => { _allSockets.Add(socket); };
                    socket.OnClose = () => { _allSockets.Remove(socket); };
                    socket.OnMessage = message =>
                    {
                        if (message == "request_status")
                        {
                            //                            var status = VPNStatus.GetVPNStatus();
                            //                            Send((int)status);
                        }
                        else
                        {
                            _allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                        }
                    };
                });
                return true;
            }
            catch (Exception e)
            {
                Log.WriteLog(e.Message);
                return false;
            }
        }

        public static void ChangePort(int port)
        {
            CloseAll();
            Start(port);
        }

        public static void Send(int status)
        {
            foreach (var socket in _allSockets.ToList())
                if (socket.IsAvailable)
                    socket.Send(status.ToString());
                else
                    socket.Close();
        }

        public static void CloseAll()
        {
            foreach (var socket in _allSockets.ToList()) socket.Close();
        }
    }
}