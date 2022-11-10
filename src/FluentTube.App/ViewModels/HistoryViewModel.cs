using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels
{
    public class HistoryViewModel
    {
        public HistoryViewModel(YouTubeServiceProvider services)
        {
            _service = services;

            LoadUserHistoryPageCommand = new AsyncRelayCommand(LoadUserHistoryPageAsync);
        }

        private readonly YouTubeServiceProvider _service;

        public IAsyncRelayCommand LoadUserHistoryPageCommand { get; }

        private async Task LoadUserHistoryPageAsync()
        {
        }
    }
}
