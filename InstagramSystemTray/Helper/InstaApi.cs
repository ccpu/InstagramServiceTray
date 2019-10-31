using System.Collections.Generic;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.SessionHandlers;
using InstagramApiSharp.Logger;
using InstagramSystemTray.Models;

namespace InstagramSystemTray.Helper
{
    public static class InstaApi
    {
        private const string StateFile = ".bin";

        public static List<InstaApiInstance> ApiInstances { get; set; }

        public static IInstaApi GetInstaApi(string userName, string password = "")
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
                .SetSessionHandler(new FileSessionHandler { FilePath = userName + StateFile })
                .Build();

            InstaApi?.SessionHandler?.Load();

            return InstaApi;
        }

        public static void SetApiInstances(IEnumerable<string> accounts)
        {
            ApiInstances = new List<InstaApiInstance>();

            foreach (var account in accounts)
            {
                var instance = GetInstaApi(account);
                if (instance != null && !instance.IsUserAuthenticated)
                    new AuthForm(account, Add).Show();
                else
                    Add(new InstaApiInstance
                    {
                        Account = account,
                        Api = instance
                    });
            }
        }
        public static void Add(InstaApiInstance apiInstance)
        {
            ApiInstances.Add(apiInstance);
        }
    }
}