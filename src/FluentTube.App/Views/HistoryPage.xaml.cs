using FluentTube.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentTube.App.Views
{
    public sealed partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<HistoryViewModel>();
        }

        public HistoryViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserHistoryPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
