using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SecureNovaPC.Utils.Converters {
    public class ThreatLevelToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string threatLevel = value as string;
            return threatLevel?.ToLower() switch {
                "critical" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000")),
                "high" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6B6B")),
                "medium" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB84D")),
                "low" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC84D")),
                _ => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4ECDC4"))
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}