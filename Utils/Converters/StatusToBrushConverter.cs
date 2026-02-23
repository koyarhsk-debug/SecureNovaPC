using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SecureNovaPC.Utils.Converters {
    public class StatusToBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string status = value as string;
            return status?.ToLower() switch {
                "protected" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4ECDC4")),
                "at risk" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6B6B")),
                "scanning" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0066CC")),
                "warning" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB84D")),
                _ => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"))
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}