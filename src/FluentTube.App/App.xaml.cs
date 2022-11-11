using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Helpers;
using CommunityToolkit.WinUI.Notifications;
using FluentTube.App;
using FluentTube.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using Microsoft.Windows.AppLifecycle;
using Serilog;
using System;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Storage;

namespace FluentTube.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedException;
        }

        public new static App Current
            => (App)Application.Current;

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
                .AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
                // ViewModels
                .AddTransient<MainViewModel>()
                .AddTransient<HomeViewModel>()
                .BuildServiceProvider();
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            var mainInstance = Microsoft.Windows.AppLifecycle.AppInstance.FindOrRegisterForKey("main");
            var activatedEventArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();

            //if (!mainInstance.IsCurrent)
            //{
            //    // Redirect the activation (and args) to the "main" instance, and exit.
            //    await mainInstance.RedirectActivationToAsync(activatedEventArgs);
            //    System.Diagnostics.Process.GetCurrentProcess().Kill();
            //    return;
            //}

            // TODO: Must be initialized in App() constructor?
            var service = await FluentTube.App.Services.YouTubeServiceProvider.GetServiceAsync();
            Services = ConfigureServices(service);

            // Initialize MainWindow here
            EnsureWindowIsInitialized();

            await Window.InitializeApplication(activatedEventArgs);
        }

        public async Task OnActivated(AppActivationArguments activatedEventArgs)
        {
            await Window.InitializeApplication(activatedEventArgs);
        }

        private void EnsureWindowIsInitialized()
        {
            Window = new MainWindow();
            //Window.Activated += Window_Activated;
            //Window.Closed += Window_Closed;
            WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(Window);
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => throw new Exception("Failed to load Page " + e.SourcePageType.FullName);

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private async void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
            => await AppUnhandledException(e.Exception);

        private async void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
            => await AppUnhandledException(e.Exception);

        private async Task AppUnhandledException(Exception ex)
        {
            try
            {
                await new Microsoft.UI.Xaml.Controls.ContentDialog
                {
                    Title = "Unhandled exception",
                    Content = ex.Message,
                    CloseButtonText = "Close"
                }
                .ShowAsync();
            }
            catch (Exception ex2)
            {
                //Services.GetService<Utils.ILogger>()?.Error("Failed to display unhandled exception", ex2);
            }
        }

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetType().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        public static void CloseApp()
            => Window.Close();

        public static AppWindow GetAppWindow(Window w)
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(w);

            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

            return AppWindow.GetFromWindowId(windowId);
        }

        public static MainWindow Window { get; private set; } = null!;

        public static IntPtr WindowHandle { get; private set; }
    }
}
