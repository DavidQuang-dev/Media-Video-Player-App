using System;
using System.Globalization;
using System.Windows.Data;

namespace video_media_player;

public class SliderValueToHeightConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double sliderValue && parameter != null && double.TryParse(parameter.ToString(), out double maxHeight))
        {
            // Calculate height based on slider value
            double height = (sliderValue / 100) * maxHeight;
            Console.WriteLine($"Slider Value: {sliderValue}, Max Height: {maxHeight}, Resulting Height: {height}");
            return height;
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
