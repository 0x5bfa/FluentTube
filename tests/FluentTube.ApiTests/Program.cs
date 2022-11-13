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

namespace FluentTube.ApiTests
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("FluentTube.ApiTests");
            Console.WriteLine("--------------------------------------------");

            try
            {
                new Program().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                    Console.WriteLine("Error: " + e.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private async Task Run()
        {
            var stream = new FileStream("AppSecrets.json", FileMode.Open, FileAccess.Read);
            ClientSecrets secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            IEnumerable<string> scopes = new[] { YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeForceSsl, YouTubeService.Scope.YoutubeReadonly };

            UserCredential credential =
                await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("FluentTube")
                );

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FluentTube"
            });

            await GetAsync(youtubeService);
        }

        static public async Task GetAsync(YouTubeService youtubeService)
        {
            var request = youtubeService.Videos.List("snippet");
            request.MyRating = VideosResource.ListRequest.MyRatingEnum.Like;
            request.MaxResults = 100;

            var response = await request.ExecuteAsync();

            foreach (var item in response.Items)
            {
                Console.WriteLine($"{item.Snippet.Title}");

            }
        }
    }
}