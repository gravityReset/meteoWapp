using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace WTW.Converter
{
    class DecimalToCelsius : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            decimal val = (decimal)value;
            long va = decimal.ToInt64(val);
                return va.ToString() + "°C";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string val = (string)value;
            val = val.Replace("°C", string.Empty);
            return decimal.Parse(val);
        }
    }
}
