using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace FluentTube.Uwp.Services
{
    public class YouTubeServiceProvider
    {
        public static ClientSecrets Secrets { get; set; }

        public static async Task<YouTubeService> GetServiceAsync()
        {
            UserCredential credential;

            using (var stream = new FileStream("AppSecrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None
                );
            }

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FluentHub"
            });
        }

        //public BaseClientService.Initializer GetServiceWithNoClient()
        //{
        //    // DISABLED FOR NOW
        //    return null;

        //    //return new BaseClientService.Initializer()
        //    //{
        //    //    ApiKey = "API_KEY",
        //    //    ApplicationName = "FluentTube"
        //    //};
        //}

        //public async Task<bool> IsUserAuthenticated()
        //{
        //    GoogleAuthorizationCodeFlow.Initializer initializer = new()
        //    {
        //        ClientSecrets = Secrets,
        //        //DataStore = new PasswordVaultDataStore(),
        //    };

        //    AuthorizationCodeFlow test = new(initializer);

        //    var token = await test.LoadTokenAsync("user", CancellationToken.None);

        //    if (token is null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
