using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace FluentTube.App.UserControls
{
    public sealed partial class VideoPlayer : UserControl
    {
        public VideoPlayer()
        {
            InitializeComponent();
        }

        #region propdp
        public static readonly DependencyProperty VideoProperty =
            DependencyProperty.Register(
                  nameof(Source),
                  typeof(string),
                  typeof(VideoPlayer),
                  new PropertyMetadata(null)
                );

        public string Source
        {
            get => (string)GetValue(VideoProperty);
            set
            {
                SetValue(VideoProperty, value);

                if (!string.IsNullOrEmpty(value))
                {
                    VideoPlayerMediaPlayerElement.Source = Windows.Media.Core.MediaSource.CreateFromUri(new(value));
                    var mediaPlayer = VideoPlayerMediaPlayerElement.MediaPlayer;
                    mediaPlayer.Play();
                }
            }
        }
        #endregion
    }
}
