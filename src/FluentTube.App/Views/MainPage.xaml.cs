using FluentTube.App.Views.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace FluentTube.App.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            MainFrame = provider.GetRequiredService<Frame>();
            MainFrame.Navigating += OnMainFrameNavigating;
        }

        private void OnMainFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            MainShellFrame.Navigate(e.SourcePageType, e.Parameter);
            e.Cancel = true;
        }

        private Frame MainFrame { get; }

        private void OnMainShellNavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
            => NavigatePageNavigationViewItemInvokedOrSelected(args.SelectedItemContainer.Tag.ToString().ToLower());

        private void OnMainShellNavigationViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
            => NavigatePageNavigationViewItemInvokedOrSelected(args.InvokedItemContainer.Tag.ToString().ToLower());

        private void NavigatePageNavigationViewItemInvokedOrSelected(string tag)
        {
            switch (tag)
            {
                default:
                case "home":
                    MainShellFrame.Navigate(typeof(HomePage));
                    break;
                case "trending":
                    MainShellFrame.Navigate(typeof(TrendingPage));
                    break;
                case "subscriptions":
                    MainShellFrame.Navigate(typeof(SubscriptionsPage));
                    break;
                case "history":
                    MainShellFrame.Navigate(typeof(HistoryPage));
                    break;
                case "watchlater":
                    MainShellFrame.Navigate(typeof(WatchLaterPage));
                    break;
                case "playlists":
                    MainShellFrame.Navigate(typeof(PlaylistsPage));
                    break;
                case "likedvideos":
                    MainShellFrame.Navigate(typeof(LikedVideosPage));
                    break;
            }
        }

        private void OnMainShellNavigationViewLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
