using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.Converter
{
    public class ChatToVisibilityConverter : IValueConverter
    {
        public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
        {
            if(value is byte b && byte.TryParse(parameter?.ToString(),out var target))
            {
                return b==target ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
