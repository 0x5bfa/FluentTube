using Google.Apis.Auth.OAuth2;
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

namespace FluentTube.Uwp.Services
{
    public class YouTubeConfigureServices
    {
        public static void SetSecrets(ClientSecrets secrets)
        {
            Secrets = secrets;
        }

        private static ClientSecrets Secrets { get; set; }

        async public Task<YouTubeService> GetServiceAsync()
        {
            if (Secrets == null)
            { 
                throw new ArgumentNullException(nameof(Secrets));
            }

            UserCredential credential;

            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                Secrets,
                new[] { YouTubeService.Scope.Youtube },
                "user",
                CancellationToken.None,
                new FileDataStore("YouTubeDataStore")
            );

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FluentTube"
            });
        }

        //public YouTubeService GetServiceNoAuth()
        //{
        //    return new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        ApiKey = "API_KEY",
        //        ApplicationName = "FluentTube"
        //    });
        //}

        //public async Task<bool> IsUserAuthenticated()
        //{
        //    GoogleAuthorizationCodeFlow.Initializer initializer = new GoogleAuthorizationCodeFlow.Initializer();
        //    var secrets = new ClientSecrets
        //    {
        //        ClientSecret = Constants.ClientSecret,
        //        ClientId = Constants.ClientID
        //    };
        //    initializer.ClientSecrets = secrets;
        //    initializer.DataStore = new PasswordVaultDataStore();
        //    var test = new AuthorizationCodeFlow(initializer);
        //    var token = await test.LoadTokenAsync("user", CancellationToken.None);
        //    if (token == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        Constants.Token = token;
        //        return true;
        //    }
        //}
    }
}
