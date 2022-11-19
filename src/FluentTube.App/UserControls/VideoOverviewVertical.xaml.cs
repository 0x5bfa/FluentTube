using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentTube.App.UserControls
{
    public sealed partial class VideoOverviewVertical : UserControl
    {
        public VideoOverviewVertical()
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
