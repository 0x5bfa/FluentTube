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

namespace FluentTube.App.Services
{
    public class YouTubeServiceProvider
    {
        public static async Task<YouTubeService> GetServiceAsync()
        {
            UserCredential credential;

            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///AppSecrets.json"));
            var stream = await file.OpenStreamForReadAsync();
            var secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            var scopes = new[]
            {
                // All
                YouTubeService.Scope.Youtube,
                YouTubeService.Scope.YoutubeChannelMembershipsCreator,
                YouTubeService.Scope.YoutubeForceSsl,
                YouTubeService.Scope.Youtubepartner,
                YouTubeService.Scope.YoutubepartnerChannelAudit,
                YouTubeService.Scope.YoutubeReadonly,
                YouTubeService.Scope.YoutubeUpload,
            };


            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                scopes,
                "user",
                CancellationToken.None
            );

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FluentHub"
            });
        }
    }
}
