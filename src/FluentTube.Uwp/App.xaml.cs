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
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            Services = ConfigureServices();
        }

        public new static App Current
            => (App)Application.Current;

        public IServiceProvider Services { get; }

        public static string AppVersion =
            $"{Package.Current.Id.Version.Major}." +
            $"{Package.Current.Id.Version.Minor}." +
            $"{Package.Current.Id.Version.Build}." +
            $"{Package.Current.Id.Version.Revision}";

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<YouTubeConfigureServices>()
                //.AddSingleton<Utils.ILogger>(new SerilogWrapperLogger(Serilog.Log.Logger))
                //.AddSingleton<ToastService>()
                //.AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
                // ViewModels
                .AddTransient<MainViewModel>()
                .AddTransient<HomeViewModel>()
                .BuildServiceProvider();
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///AppSecrets.json"));
            var stream = await file.OpenStreamForReadAsync();
            var secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            YouTubeConfigureServices.SetSecrets(secrets);

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
