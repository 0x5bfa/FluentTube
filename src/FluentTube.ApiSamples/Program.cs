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

namespace FluentTube.ApiSample
{
    public class CommentInfo
    {
        public int ParentNo;
        public int ChildNo;
        public string Text;
        public long LikeCount;
        public string AuthorName;
        public DateTime PublishedAt;
        public long ReplyCount;
    }

    public class VideoComment
    {
        private const string API_KEY = "AIzaSyA0OlZkxQ7lilWonkgYyaeFx3G1py4bcPk";
        private const string VIDEO_ID = "Y8JFxS1HlDo";

        static void Main(string[] args)
        {
            Console.WriteLine("FluentTube.Apis.Sample");
            Console.WriteLine("--------------------------------------------");

            try
            {
                new VideoComment().Run().Wait();
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
            UserCredential credential;

            using (var stream = new FileStream("AppSecrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            var videoId = VIDEO_ID;

            await GetComment(videoId, youtubeService, 1, null);
        }

        static public async Task GetComment(string videoId, YouTubeService youtubeService, int no, string nextPageToken)
        {
            List<CommentInfo> commentList = new List<CommentInfo>();

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
