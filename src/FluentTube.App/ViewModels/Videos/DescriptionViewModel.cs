using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels.Videos
{
    public class DescriptionViewModel : ObservableObject
    {
        public DescriptionViewModel(YouTubeService services)
        {
            _service = services;

            LoadDescriptionPageCommand = new AsyncRelayCommand(LoadDescriptionPageAsync);
        }

        private readonly YouTubeService _service;

        private string _videoId;
        public string VideoId { get => _videoId; set => SetProperty(ref _videoId, value); }

        private Video _video;
        public Video Video { get => _video; set => SetProperty(ref _video, value); }

        public IAsyncRelayCommand LoadDescriptionPageCommand { get; }

        private async Task LoadDescriptionPageAsync()
        {
            // Gets video specified by VideoId
            var requestVideo = _service.Videos.List(new[] { "snippet", "statistics" });
            requestVideo.Id = VideoId;
            var responseVideo = await requestVideo.ExecuteAsync();
            Video = responseVideo.Items.First();
        }
    }
}
