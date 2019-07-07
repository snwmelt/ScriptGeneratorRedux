using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ScriptGeneratorRedux.Views.Helpers.Converters
{
    internal class BooleanToVisibilityConverter : IValueConverter
    {
        public Object Convert( Object Value, Type TargetType, Object Parameter, CultureInfo Culture )
        {
            return ( Value as Boolean? ).HasValue ? ( ( Value as Boolean? ).Value ? Visibility.Visible 
                                                                                  : Visibility.Collapsed )
                                                  : Visibility.Collapsed;
        }

        public Object ConvertBack( Object Value, Type TargetType, Object Parameter, CultureInfo Culture )
        {
            return ( Value is Visibility ) ? ( ( ( Visibility )Value == Visibility.Visible ) ? true
                                                                                             : false )
                                           : false;
        }
    }
}
