using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace MediaApp.Converters
{
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var container = value as FrameworkElement;
            if (container != null)
            {
                var itemsControl = ItemsControl.ItemsControlFromItemContainer(container);
                int index = itemsControl.ItemContainerGenerator.IndexFromContainer(container);
                return (index + 1).ToString(); // Số thứ tự (bắt đầu từ 1)
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
