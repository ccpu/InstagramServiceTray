using System.Collections.Generic;

namespace InstagramSystemTray.Models
{
    public class Setting
    {
        public Setting()
        {
            Accounts = new List<string>();
        }
        public int WebSocketPort { get; set; }
        public List<string> Accounts { get; set; }

    }
}
