using FluentTube.App.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace FluentTube.App.Views.Users
{
    public sealed partial class TrendingPage : Page
    {
        public TrendingPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            MainFrame = provider.GetRequiredService<Frame>();
            ViewModel = provider.GetRequiredService<TrendingViewModel>();
        }

        private Frame MainFrame { get; }
        private TrendingViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserTrendingVideosPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnVideoOverviewVerticalClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Videos.PlayerPage), (sender as UserControls.VideoOverviewVertical).Tag as string);
        }
    }
}
