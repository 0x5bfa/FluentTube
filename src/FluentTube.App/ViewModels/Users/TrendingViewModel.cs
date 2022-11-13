using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels.Users
{
    public class TrendingViewModel : ObservableObject
    {
        public TrendingViewModel(YouTubeService services)
        {
            _service = services;

            _trendingVideos = new();
            TrendingVideos = new(_trendingVideos);

            LoadUserTrendingVideosPageCommand = new AsyncRelayCommand(LoadUserTrendingVideosPageAsync);
        }

        private readonly YouTubeService _service;

        private readonly ObservableCollection<Video> _trendingVideos;
        public ReadOnlyObservableCollection<Video> TrendingVideos { get; }

        public IAsyncRelayCommand LoadUserTrendingVideosPageCommand { get; }

        private async Task LoadUserTrendingVideosPageAsync()
        {
            var request = _service.Videos.List(new[] { "snippet", "statistics" });
            request.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
            request.MaxResults = 100;
            request.RegionCode = "jp";

            var response = await request.ExecuteAsync();

            foreach (var item in response.Items)
            {
                _trendingVideos.Add(item);
            }
        }
    }
}
