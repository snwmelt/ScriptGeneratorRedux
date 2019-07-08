using System;
using System.Security.Cryptography;
using System.Text;

namespace ScriptGeneratorRedux.Models.Extensions
{
    internal static class ExString
    {
        public static String EntangledUpdate( this String Target, String Source )
        {
            return ( Source.Length > Target.Length ? Target + Source.Substring( Source.Length - 1 ) : Target.Substring( 0, Source.Length ) );
        }

        /// <summary>
        /// Generates a 256 bit System.Byte[] representation of the a string combined with a salt.
        /// </summary>
        /// <param name="Target">System.String Containing Target String.</param>
        /// <param name="Salt">System.String Containing Salt To Combine With The Target String.</param>
        /// <returns>Resulting Syste.Byte[] Key From Rfc2898DeriveBytes( string, byte[], int )</returns>
        public static Byte[ ] Generate256BitKey( this String Target, String Salt )
        {
            return ( new Rfc2898DeriveBytes( Target, Encoding.ASCII.GetBytes( Salt ), 300 ) ).GetBytes( 32 );
        }

        /// <summary>
        /// Generates an SHA256 from the System.Array Instance passed by the user.
        /// </summary>
        /// <param name="This">System.String Instance To Be Operated On.</param>
        /// <returns>String Containing SHA256 Hash Of This String.</returns>
        public static String GetSHA256Hash( this String This )
        {
            StringBuilder _StringBuilder = new StringBuilder( );

            using( var _Hash = SHA256.Create( ) )
            {
                Encoding _Encoding = Encoding.UTF8;

                Byte[ ] _Result = _Hash.ComputeHash( _Encoding.GetBytes( This ) );

                foreach( Byte _Byte in _Result )
                {
                    _StringBuilder.Append( _Byte.ToString( "x2" ) );
                }
            }

            return _StringBuilder.ToString( );
        }
    }
}
