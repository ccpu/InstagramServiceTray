using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.SessionHandlers;
using InstagramApiSharp.Logger;

namespace InstagramSystemTray.Helper
{
    public static class InstaApi
    {
        private const string StateFile = "state.bin";

        public static IInstaApi GetInstaApi(string userName = "", string password = "")
        {
            var userSession = new UserSessionData
            {
                UserName = userName,
                Password = password
            };

            var InstaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                // Session handler, set a file path to save/load your state/session data
                .SetSessionHandler(new FileSessionHandler { FilePath = StateFile })
                .Build();
            InstaApi?.SessionHandler?.Load();
            return InstaApi;
        }
    }
}