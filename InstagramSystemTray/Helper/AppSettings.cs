using System.IO;
using InstagramSystemTray.Constants;
using InstagramSystemTray.Models;
using Newtonsoft.Json;

namespace InstagramSystemTray.Helper
{
    public static class AppSettings
    {
        private static readonly string path = "setting.json";

        public static Setting GetSetting()
        {
            if (!File.Exists(path)) return new Setting { WebSocketPort = Common.ServerPort };

            using (var r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Setting>(json);
                return items;
            }
        }

        public static void SetSetting(Setting setting)
        {
            using (var file = File.CreateText(path))
            {
                var serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, setting);
            }
        }

        public static string ValidateSetting(Setting sett)
        {


            if (sett.WebSocketPort < 1024 || sett.WebSocketPort > 49151)
                return "Port must be between 1024 and 49151.";

            if (sett.Accounts == null || sett.Accounts.Count < 1)
                return "Please provide instagram account(s).";

            return null;
        }

        internal static bool IsValidSetting()
        {
            var sett = GetSetting();
            var message = ValidateSetting(sett);
            return string.IsNullOrEmpty(message);
        }
    }
}