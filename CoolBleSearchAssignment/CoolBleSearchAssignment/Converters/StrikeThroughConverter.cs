using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CoolBleSearchAssignment.Converters
{
    public class StrikeThroughConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value != 0 ? $"{ConvertToStrikethrough(value.ToString())}" : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private string ConvertToStrikethrough(string stringToChange)
        {
            var newString = "";
            stringToChange.ForEach(character => newString += $"{character}\u0336");
            return newString;
        }
    }
}
