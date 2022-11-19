using FluentTube.App.ViewModels.Videos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace FluentTube.App.Views.Videos
{
    public sealed partial class SuggestionsPage : Page
    {
        public SuggestionsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            MainFrame = provider.GetRequiredService<Frame>();
            ViewModel = provider.GetRequiredService<SuggestionsViewModel>();
        }

        private Frame MainFrame { get; }
        private SuggestionsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string videoId = e.Parameter as string;

            ViewModel.VideoId = videoId;

            var command = ViewModel.LoadSuggestionsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnVideoOverviewHorizontalClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Videos.PlayerPage), (sender as UserControls.VideoOverviewHorizontal).Tag as string);
        }
    }
}
