using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace FluentTube.App.ViewModels.Videos
{
    public class PlayerViewModel : ObservableObject
    {
        public PlayerViewModel(YouTubeService services)
        {
            _service = services;

            LoadVideoPlayerPageCommand = new AsyncRelayCommand(LoadVideoPlayerPageAsync);
        }

        private readonly YouTubeService _service;

        private string _videoId;
        public string VideoId { get => _videoId; set => SetProperty(ref _videoId, value); }

        private Video _video;
        public Video Video { get => _video; set => SetProperty(ref _video, value); }

        private Channel _channel;
        public Channel Channel { get => _channel; set => SetProperty(ref _channel, value); }

        private string _videoRowUrl;
        public string VideoRowUrl { get => _videoRowUrl; set => SetProperty(ref _videoRowUrl, value); }

        public IAsyncRelayCommand LoadVideoPlayerPageCommand { get; }

        private async Task LoadVideoPlayerPageAsync()
        {
            /// YouTube Official API

            // Gets video specified by VideoId
            var requestVideo = _service.Videos.List(new[] { "snippet", "statistics" });
            requestVideo.Id = VideoId;
            var responseVideo = await requestVideo.ExecuteAsync();
            Video = responseVideo.Items.First();

            // Gets channel that is publisher of the Video
            var requestChannel = _service.Channels.List(new[] { "snippet", "statistics" });
            requestChannel.Id = Video.Snippet.ChannelId;
            var responseChannel = await requestChannel.ExecuteAsync();
            Channel = responseChannel.Items.First();

            /// YouTube Explode API
            
            // Gets highest quality video link
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VideoId);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            VideoRowUrl = streamInfo.Url;
        }
    }
}
