using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SecureNovaPC.Utils.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public bool IsInversed { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (IsInversed)
            {
                boolValue = !boolValue;
            }
            return boolValue ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}