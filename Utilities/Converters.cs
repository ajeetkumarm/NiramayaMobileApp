using System.Globalization;

namespace Nirmaya.Utilities
{
    public class Converters
    {
        public class StringToBoolConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is string stringValue)
                {
                    return !string.IsNullOrWhiteSpace(stringValue);
                }
                return false;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class InvertedBoolConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool boolValue)
                {
                    return !boolValue;
                }
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool boolValue)
                {
                    return !boolValue;
                }
                return value;
            }
        }
    }
}
