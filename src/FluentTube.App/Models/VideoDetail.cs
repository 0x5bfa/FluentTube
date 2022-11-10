using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentTube.App.Models
{
    public class VideoDetail
    {
        public BitmapImage Videoimage { get; set; }

        public BitmapImage Header { get; set; }

        public string title { get; set; }

        public string Name { get; set; }

        public int Views { get; set; }

        public string Time { get; set; }
    }
}
