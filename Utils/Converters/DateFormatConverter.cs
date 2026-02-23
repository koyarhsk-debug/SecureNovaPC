using System;
using System.Globalization;
using System.Windows.Data;

namespace SecureNovaPC.Utils.Converters
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                string format = parameter as string ?? "dd/MM/yyyy HH:mm:ss";
                return dateTime.ToString(format);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value as string, out DateTime result))
            {
                return result;
            }
            return DateTime.Now;
        }
    }
}