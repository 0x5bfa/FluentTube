using FluentTube.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace FluentTube.App.Views
{
    public sealed partial class LikedVideosPage : Page
    {
        public LikedVideosPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<LikedVideosViewModel>();
        }

        public LikedVideosViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserLikedVideosPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
