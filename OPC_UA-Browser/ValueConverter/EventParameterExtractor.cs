using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OPC_UA_Browser.ValueConverter;

public class EventParameterExtractor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value is RoutedPropertyChangedEventArgs<object> propertyArgs)
            return propertyArgs.NewValue;


        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}