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
    public sealed partial class VideoOverviewHorizontal : UserControl
    {
        public VideoOverviewHorizontal()
        {
            InitializeComponent();
        }

        #region propdp
        public static readonly DependencyProperty VideoProperty =
            DependencyProperty.Register(
                  nameof(Video),
                  typeof(Video),
                  typeof(VideoOverviewVertical),
                  new PropertyMetadata(null)
                );

        public Video Video
        {
            get => (Video)GetValue(VideoProperty);
            set
            {
                SetValue(VideoProperty, value);
            }
        }
        #endregion

        public event RoutedEventHandler Click;

        private void OnButtonClick(object sender, RoutedEventArgs e)
            => Click?.Invoke(this, e);
    }
}
