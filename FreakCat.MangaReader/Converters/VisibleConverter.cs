using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FreakCat.MangaReader.Converters
{
    class VisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool) value)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Visibility) value == Visibility.Visible;
        }
    }
}
