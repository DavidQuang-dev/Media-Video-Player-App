using System;
using System.Globalization;
using System.Windows.Data;

namespace video_media_player;

public class SliderValueToHeightConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double sliderValue && parameter is string maxHeightStr && double.TryParse(maxHeightStr, out double maxHeight))
        {
            double maxSliderValue = 10; // Maximum value of the slider
            return (sliderValue / maxSliderValue) * maxHeight;
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
