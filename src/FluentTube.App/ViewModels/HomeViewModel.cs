using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        public HomeViewModel(YouTubeService services)
        {
            _service = services;

            _homeActivities = new();
            HomeActivities = new(_homeActivities);

            LoadUserHomeActivitiesPageCommand = new AsyncRelayCommand(LoadUserHomeActivitiesPageAsync);
        }

        private readonly YouTubeService _service;

        private readonly ObservableCollection<Activity> _homeActivities;
        public ReadOnlyObservableCollection<Activity> HomeActivities { get; }

        public IAsyncRelayCommand LoadUserHomeActivitiesPageCommand { get; }

        private async Task LoadUserHomeActivitiesPageAsync()
        {
            var request = _service.Activities.List("snippet");
            request.Home = true;

            var response = await request.ExecuteAsync();

            foreach (var item in response.Items)
            {
                _homeActivities.Add(item);
            }
        }
    }
}
