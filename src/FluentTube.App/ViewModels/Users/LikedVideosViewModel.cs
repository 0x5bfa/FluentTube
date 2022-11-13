using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels.Users
{
    public class LikedVideosViewModel
    {
        public LikedVideosViewModel(YouTubeService services)
        {
            _service = services;

            LoadUserLikedVideosPageCommand = new AsyncRelayCommand(LoadUserLikedVideosPageAsync);
        }

        private readonly YouTubeService _service;

        public IAsyncRelayCommand LoadUserLikedVideosPageCommand { get; }

        private async Task LoadUserLikedVideosPageAsync()
        {
        }
    }
}
