using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace WTW.Converter
{
    public sealed class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string uri = (string)value;
            uri = @"ms-appx:///" + uri;
            return toBitmap(uri);
        }

        private static object toBitmap(string value)
        {
            try
            {
                return new BitmapImage(new Uri(value));
            }
            catch (Exception)
            {
                return new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
