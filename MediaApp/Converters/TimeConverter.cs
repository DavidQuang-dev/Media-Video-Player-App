using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MediaApp.Converters
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                // Chuyển đổi decimal thành TimeSpan (hoặc cách bạn muốn)
                return convertTimeFormat(decimalValue);
            }
            return "Thoi gian bi sai"; // Giá trị mặc định
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string convertTimeFormat(decimal value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds((double)value);
            string timeFormatted = string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return timeFormatted;
        }
    }
}
