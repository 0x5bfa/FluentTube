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

namespace FluentTube.Apis.Sample
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
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for full read/write access to the
                    // authenticated user's account.
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
            //var request = youtubeService.CommentThreads.List("snippet");
            //request.VideoId = videoId;
            //request.Order = CommentThreadsResource.ListRequest.OrderEnum.Relevance;
            //request.TextFormat = CommentThreadsResource.ListRequest.TextFormatEnum.PlainText;
            //request.PageToken = nextPageToken;

            //var response = await request.ExecuteAsync();
            //foreach (var item in response.Items)
            //{
            //    try
            //    {
            //        CommentInfo info = new CommentInfo();
            //        info.ParentNo = no;
            //        info.ChildNo = 0;
            //        info.Text = item.Snippet.TopLevelComment.Snippet.TextDisplay;
            //        info.LikeCount = (long)item.Snippet.TopLevelComment.Snippet.LikeCount;
            //        info.AuthorName = item.Snippet.TopLevelComment.Snippet.AuthorDisplayName;
            //        if (item.Snippet.TopLevelComment.Snippet.PublishedAt != null)
            //            info.PublishedAt = (DateTime)item.Snippet.TopLevelComment.Snippet.PublishedAt;
            //        info.ReplyCount = (long)item.Snippet.TotalReplyCount;
            //        string parentId = item.Snippet.TopLevelComment.Id;
            //        commentList.Add(info);

            //        if (item.Snippet.TotalReplyCount > 0)
            //            await GetReplyComment(commentList, youtubeService, parentId, no, 1, null);
            //        no++;
            //    }
            //    catch { }
            //}

            //if (response.NextPageToken != null)
            //    await GetComment(videoId, youtubeService, no, response.NextPageToken);
        }
    }
}
