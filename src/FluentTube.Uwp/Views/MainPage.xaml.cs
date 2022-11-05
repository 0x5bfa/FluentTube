using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentTube.Uwp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnMainShellNavigationViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            var tag = args.InvokedItemContainer.Tag.ToString().ToLower();

            switch (tag)
            {
                default:
                case "home":
                    MainShellFrame.Navigate(typeof(HomePage));
                    break;
                case "shorts":
                    break;
                case "subscriptions":
                    break;
                case "library":
                    MainShellFrame.Navigate(typeof(LibraryPage));
                    break;
                case "history":
                    MainShellFrame.Navigate(typeof(HistoryPage));
                    break;
                case "watchlater":
                    break;
                case "likedvideos":
                    MainShellFrame.Navigate(typeof(LikedVideosPage));
                    break;
            }
        }

        private void OnMainShellNavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            var defaultItem
                = MainShellNavigationView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault();

            MainShellNavigationView.SelectedItem
                = MainShellNavigationView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString().ToLower(), "home", true) == 0)
                ?? defaultItem;
        }
    }
}
