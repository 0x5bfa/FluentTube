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

        private string _videoRowUrl;
        public string VideoRowUrl { get => _videoRowUrl; set => SetProperty(ref _videoRowUrl, value); }

        public IAsyncRelayCommand LoadVideoPlayerPageCommand { get; }

        private async Task LoadVideoPlayerPageAsync()
        {
            // YouTube Official API
            var request = _service.Videos.List(new[] { "snippet", "statistics" });
            request.Id = VideoId;
            var response = await request.ExecuteAsync();
            Video = response.Items.First();

            // YouTube Explode API
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VideoId);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            VideoRowUrl = streamInfo.Url;
        }
    }
}
