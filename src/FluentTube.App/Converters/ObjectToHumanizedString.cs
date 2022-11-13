using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace FluentTube.App.Converters
{
    public class ObjectToHumanizedString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = "";
            string valStr = value.ToString();

            if (parameter is not null)
            {
                value = valStr;
            }

            switch (value)
            {
                case DateTime dt:
                    result = dt.Humanize();
                    break;
                case DateTimeOffset dto:
                    result = dto.Humanize();
                    break;
                case string str:
                    switch (parameter.ToString().ToLower())
                    {
                        case "metric":
                            // DANGER TO BE FIXED: Could ulong to int
                            int val = System.Convert.ToInt32(str);
                            result = val.ToMetric(null, 1);
                            break;
                    }
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
