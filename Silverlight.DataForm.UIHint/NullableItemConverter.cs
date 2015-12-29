using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Silverlight.DataForm.UIHint
{
    public class NullableItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var comboItem = value as ComboBoxItem;
            if (comboItem != null)
            {
                return comboItem.DataContext;
            }

            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var comboItem = value as ComboBoxItem;
            if (comboItem != null)
            {
                return comboItem.DataContext;
            }

            else
            {
                return value;
            }
        }
    }
}