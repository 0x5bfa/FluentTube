using FluentTube.App.ViewModels.Videos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace FluentTube.App.Views.Videos
{
    public sealed partial class CommentsPage : Page
    {
        public CommentsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<CommentsViewModel>();
        }

        public CommentsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string videoId = e.Parameter as string;

            ViewModel.VideoId = videoId;

            var command = ViewModel.LoadCommentsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
