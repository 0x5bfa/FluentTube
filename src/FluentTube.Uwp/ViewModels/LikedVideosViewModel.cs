using FluentTube.Uwp.Models;
using FluentTube.Uwp.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentTube.Uwp.ViewModels
{
    public class LikedVideosViewModel
    {
        public LikedVideosViewModel(YouTubeServiceProvider services)
        {
            _service = services;

            LoadUserLikedVideosPageCommand = new AsyncRelayCommand(LoadUserLikedVideosPageAsync);
        }

        private readonly YouTubeServiceProvider _service;

        public IAsyncRelayCommand LoadUserLikedVideosPageCommand { get; }

        private async Task LoadUserLikedVideosPageAsync()
        {
        }
    }
}
