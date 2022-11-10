using FluentTube.Uwp.Services;
using FluentTube.Uwp.ViewModels;
using FluentTube.Uwp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentTube.Uwp
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        public new static App Current
            => (App)Application.Current;

        // This service provider will not be initialized when App() is initialized.
        // Instead, this will be initialized on launched.
        public IServiceProvider Services { get; set; }

        public static string AppVersion =
            $"{Package.Current.Id.Version.Major}." +
            $"{Package.Current.Id.Version.Minor}." +
            $"{Package.Current.Id.Version.Build}." +
            $"{Package.Current.Id.Version.Revision}";

        private IServiceProvider ConfigureServices(YouTubeService ytSrvice)
        {
            return new ServiceCollection()
                .AddSingleton(ytSrvice)
                // View models
                .AddTransient<HistoryViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<LibraryViewModel>()
                .AddTransient<LikedVideosViewModel>()
                .AddTransient<MainViewModel>()
                .BuildServiceProvider();
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            var service = await Core.ServiceProvider.GetServiceAsync();

            // TODO: Must be initialized in App() constructor?
            ConfigureServices(service);

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }
    }
}
