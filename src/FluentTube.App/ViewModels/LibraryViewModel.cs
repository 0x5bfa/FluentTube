using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels
{
    public class LibraryViewModel
    {
        public LibraryViewModel(YouTubeService services)
        {
            _service = services;

            LoadUserHomeActivitiesPageCommand = new AsyncRelayCommand(LoadUserHomeActivitiesPageAsync);
        }

        private readonly YouTubeService _service;

        public IAsyncRelayCommand LoadUserHomeActivitiesPageCommand { get; }

        private async Task LoadUserHomeActivitiesPageAsync()
        {
        }
    }
}
