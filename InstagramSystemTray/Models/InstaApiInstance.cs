using InstagramApiSharp.API;

namespace InstagramSystemTray.Models
{
    public class InstaApiInstance
    {
        public string Account { get; set; }
        public IInstaApi Api { get; set; }
    }
}