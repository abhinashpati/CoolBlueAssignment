using CoolBleSearchAssignment.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CoolBleSearchAssignment.Converters
{
    public class PromotionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value as string) ? (value as string).Contains("coolblues") ? AppConstant.COOL_BLUE_CHOICE : AppConstant.COOL_BLUE_PROMOTION : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
