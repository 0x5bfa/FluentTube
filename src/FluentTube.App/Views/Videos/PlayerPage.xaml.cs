using FluentTube.App.ViewModels.Videos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Numerics;

namespace FluentTube.App.Views.Videos
{
    public sealed partial class PlayerPage : Page
    {
        public PlayerPage()
        {
            InitializeComponent();

            PlayerBorder.Translation += new Vector3(0, 0, 32);

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<PlayerViewModel>();
        }

        public PlayerViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string videoId = e.Parameter as string;

            ViewModel.VideoId = videoId;

            var command = ViewModel.LoadVideoPlayerPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
