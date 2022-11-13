using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace FluentTube.App.Converters
{
    public class DoubleToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string valStr = value?.ToString();
            if (valStr == "NaN")
                return new GridLength(48);

            return new GridLength((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
