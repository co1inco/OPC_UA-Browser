using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Opc.Ua;

namespace OPC_UA_Browser.ValueConverter;

public class NodeClassToIconConverter : IValueConverter
{

    public NodeClassToIconConverter()
    {
        _variableImageSource = new BitmapImage(new Uri("pack://application:,,,/OPC_UA-Browser;component/Icons/icons8-subscript-24.png", UriKind.Absolute));
        _classImageSource = new BitmapImage(new Uri("pack://application:,,,/OPC_UA-Browser;component/Icons/icons8-classroom-24.png", UriKind.Absolute));
        _functionImageSource = new BitmapImage(new Uri("pack://application:,,,/OPC_UA-Browser;component/Icons/icons8-lambda-24.png", UriKind.Absolute));
        _viewImageSource = new BitmapImage(new Uri("pack://application:,,,/OPC_UA-Browser;component/Icons/icons8-static-view-level2-24.png", UriKind.Absolute));

    }


    private BitmapImage _variableImageSource;
    private BitmapImage _classImageSource;
    private BitmapImage _functionImageSource;
    private BitmapImage _viewImageSource;



    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //return new Uri("Icons/icons8-variable-24.png", UriKind.Relative);


        if (!(value is NodeClass nodeClass))
            return value;



        switch (nodeClass)
        {
            case NodeClass.Unspecified:
                break;
            case NodeClass.Object:
                return _classImageSource;
            case NodeClass.Variable:
                return _variableImageSource;
            case NodeClass.Method:
                return _functionImageSource;
            case NodeClass.ObjectType:
                return _classImageSource;
            case NodeClass.VariableType:
                return _variableImageSource;
            case NodeClass.ReferenceType:
                break;
            case NodeClass.DataType:
                break;
            case NodeClass.View:
                return _viewImageSource;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}