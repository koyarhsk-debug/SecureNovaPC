using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SecureNovaPC.Utils.Converters {
    public class BooleanToVisibilityConverter : IValueConverter {
        public bool IsInversed { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool boolValue = (bool)value;
            if (IsInversed) {
                boolValue = !boolValue;
            }
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            Visibility visibility = (Visibility)value;
            bool result = visibility == Visibility.Visible;
            if (IsInversed) {
                result = !result;
            }
            return result;
        }
    }
}