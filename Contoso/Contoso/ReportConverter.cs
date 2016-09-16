using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Contoso
{
    public class ReportConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            return (ReportViewModel)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (object)value;
        }
    }
}
