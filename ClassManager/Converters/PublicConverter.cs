using ClassManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace ClassManager.Converters
{
    class PublicConverter
    {
    }

    public class NameToNameCVT : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null || (string)value == "" ? "（还没有设置）" : (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateCVT : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string date_str = (string)value;
                date_str = date_str.Substring(0, 19);
                DateTime dt = DateTime.ParseExact(date_str, "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                dt = dt.AddHours(16);
                return string.Format("{0:U}", dt);
            }
            catch (Exception e)
            {
                return "（时间显示异常）";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class UrlToSourceCVT : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string image_str = value == null ? "null" : (string)value;
            return new BitmapImage(new Uri(SingleService.Instance.getServerAddress() + (string)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
