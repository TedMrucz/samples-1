using System;
using System.Windows.Data;
using System.Windows.Controls;
using System.Globalization;
using System.Collections;

//this is to avoid silence exception when Errors collection is empty and element{0} is accessed
namespace DecisionPoint.Converters
{
    [ValueConversion(typeof(IList), typeof(String))]
    public class CollectionToErrorContent : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string retyrnValue = string.Empty;

            IList collection = null;
            if (value != null)
            {
                collection = value as IList;
                if (collection != null && 0 < collection.Count)
                {
                    ValidationError validationError = collection[0] as ValidationError;
                    if (validationError != null)
                        retyrnValue = validationError.ErrorContent.ToString();
                }
            }
            return retyrnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}