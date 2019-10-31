using System;
using System.IO;
using System.Text;

namespace InstagramSystemTray.Helper
{
    public static class Log
    {
        public static void WriteLog(string log)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongTimeString() + ": " + log + Environment.NewLine);
            File.AppendAllText("log.txt", sb.ToString());
            sb.Clear();
        }
    }
}
