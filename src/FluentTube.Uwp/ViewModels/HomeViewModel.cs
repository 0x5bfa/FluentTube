using FluentTube.Uwp.Models;
using FluentTube.Uwp.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentTube.Uwp.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        public HomeViewModel(YouTubeConfigureServices services)
        {
            VideoDetails = new ObservableCollection<VideoDetail>();

            for (int i = 0; i < 100; i++)
            {
                VideoDetails.Add(new VideoDetail()
                {
                    Videoimage = new BitmapImage(new Uri("ms-appx:///Assets/panda.jpg")),
                    Header = new BitmapImage(new Uri("ms-appx:///Assets/monkey.jpg")),
                    Name = "author",
                    title = "this is a video from test website",
                    Time = DateTime.Now.Day.ToString(),
                    Views = 9000
                });
            }
        }

        public ObservableCollection<VideoDetail> VideoDetails { get; set; }
    }
}
