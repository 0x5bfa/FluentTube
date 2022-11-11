﻿using FluentTube.Uwp.Models;
using FluentTube.Uwp.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentTube.Uwp.ViewModels
{
    public class LibraryViewModel
    {
        public LibraryViewModel(YouTubeServiceProvider services)
        {
            _service = services;

            LoadUserHomeActivitiesPageCommand = new AsyncRelayCommand(LoadUserHomeActivitiesPageAsync);
        }

        private readonly YouTubeServiceProvider _service;

        public IAsyncRelayCommand LoadUserHomeActivitiesPageCommand { get; }

        private async Task LoadUserHomeActivitiesPageAsync()
        {
        }
    }
}