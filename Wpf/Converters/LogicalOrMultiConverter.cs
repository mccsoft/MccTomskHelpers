using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfInfrastructure.Converters
{
    public class LogicalOrMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var value in values)
            {
                if (!(value is Boolean))
                    return false;
                if (!(Boolean)value)
                    return false;
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
