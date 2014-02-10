using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace WTW.Converter
{
    class nullToEnable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (string.IsNullOrWhiteSpace((string)value))
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
