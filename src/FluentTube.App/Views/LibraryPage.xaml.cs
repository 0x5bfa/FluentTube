using FluentTube.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentTube.App.Views
{
    public sealed partial class LibraryPage : Page
    {
        public LibraryPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<LibraryViewModel>();
        }

        public LibraryViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserHomeActivitiesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
