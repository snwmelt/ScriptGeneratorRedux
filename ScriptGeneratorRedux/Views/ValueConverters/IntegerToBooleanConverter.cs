using System;
using System.Globalization;
using System.Windows.Data;

namespace ScriptGeneratorRedux.Views.ValueConverters
{
    internal sealed class IntegerToBooleanConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( !( value is int ) )
                throw new InvalidOperationException( "Target Type Must be an int." );

            return ( ( int )value < 0 ) ? true
                                        : false;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException( );
        }
    }
}
