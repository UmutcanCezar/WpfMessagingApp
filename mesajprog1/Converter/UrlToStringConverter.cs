using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace mesajprog1.Converter
{
    public class UrlToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = value as string;
            if (string.IsNullOrWhiteSpace(url))
                url = "pack://application:,,,/WpfMessagingApp;component/Assets/profilphoto.png";

            try
            {
                return new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
            }
            catch
            {
                return new BitmapImage(new Uri("pack://application:,,,/mesajprog1;component/Assets/profilphoto.png"));
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
