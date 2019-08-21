using System;
using System.Globalization;
using System.Windows.Data;

namespace ScriptGeneratorRedux.Views.ValueConverters
{
    internal sealed class MathConverter : IValueConverter
    {
        public Object Convert( Object value, Type targetType, Object parameter, CultureInfo culture )
        {
            if( value is int )
                return ( int )value / 2;

            if( value is double || value is float )
                return ( double )value / 2;

            return null;
        }

        public Object ConvertBack( Object value, Type targetType, Object parameter, CultureInfo culture )
        {
            throw new NotImplementedException( );
        }
    }
}
