using System;
using System.Globalization;
using System.Windows.Data;
namespace SecureNovaPC.Utils.Converters {
    public class ValueToPercentageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is double doubleValue) {
                return Math.Round(doubleValue, 2).ToString() + "%";
            }
            return "0%";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}