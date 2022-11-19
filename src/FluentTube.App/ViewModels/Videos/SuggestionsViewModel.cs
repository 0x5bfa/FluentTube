using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels.Videos
{
    public class SuggestionsViewModel : ObservableObject
    {
        public SuggestionsViewModel(YouTubeService services)
        {
            _service = services;

            _suggestedVideos = new();
            SuggestedVideos = new(_suggestedVideos);

            LoadSuggestionsPageCommand = new AsyncRelayCommand(LoadSuggestionsPageAsync);
        }

        private readonly YouTubeService _service;

        private string _videoId;
        public string VideoId { get => _videoId; set => SetProperty(ref _videoId, value); }

        private readonly ObservableCollection<Video> _suggestedVideos;
        public ReadOnlyObservableCollection<Video> SuggestedVideos { get; }

        public IAsyncRelayCommand LoadSuggestionsPageCommand { get; }

        private async Task LoadSuggestionsPageAsync()
        {
            // Gets video ids specified by VideoId
            var requestSearch = _service.Search.List("id");
            requestSearch.MaxResults = 50;
            requestSearch.RelatedToVideoId = VideoId;
            requestSearch.Type = "video";
            var responseSearch = await requestSearch.ExecuteAsync();

            // Gets video for each video ids
            var requestVideo = _service.Videos.List(new[] { "snippet", "statistics" });
            requestVideo.Id = new(responseSearch.Items.Select(x => x.Id.VideoId));
            var responseVideo = await requestVideo.ExecuteAsync();

            foreach (var item in responseVideo.Items)
            {
                _suggestedVideos.Add(item);
            }
        }
    }
}
