using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

public class SongSelectionConverter : IValueConverter
{
    public HashSet<int> SelectedSongIds { get; set; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int songId)
        {
            return SelectedSongIds.Contains(songId);
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Not used in this scenario
        throw new NotImplementedException();
    }
}
