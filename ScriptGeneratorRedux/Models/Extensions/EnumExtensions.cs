using System;
using System.ComponentModel;
using System.Reflection;

namespace ScriptGeneratorRedux.Models.Extensions
{
    internal static class EnumExtensions
    {
        /// <summary>
        /// Evaluates an System.Enum instance, returning the System.String from the Description Attribute 
        /// decoration if available otherwise returning the System.String from the ToString() method.
        /// </summary>
        /// <param name="This">System.Enum Instance To Be Operated On.</param>
        /// <returns>System.String From The Description Attribute Decorating The System.Enum.</returns>
        public static String GetDescription( this System.Enum This )
        {
            MemberInfo[ ] _MemberInfo = This.GetType( ).GetMember( This.ToString( ) );

            if( _MemberInfo.Length > 0 )
            {
                dynamic[ ] _CustomAttributes = _MemberInfo[ 0 ].GetCustomAttributes( typeof( DescriptionAttribute ), false );

                if( _CustomAttributes.Length > 0 )
                {
                    return _CustomAttributes[ 0 ].Description;
                }
            }

            return This.ToString( );
        }
    }
}
