using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLConnectionCredentials : ISQLConnectionCredentials
    {
        #region Private Variables
        private String _ConnectionString;
        #endregion

        public SQLConnectionCredentials( String ConnectionString, Boolean PersistSecurityInfo = true, String Username = null, String Password = null )
        {
            if( String.IsNullOrWhiteSpace( Username ) )
                throw new ArgumentOutOfRangeException( "Username Cannot Be Null Or Whitespace." );

            if( String.IsNullOrWhiteSpace( ConnectionString ) )
                throw new ArgumentOutOfRangeException( "SQL Connection String Cannot Be Null Or Whitespace." );

            this.ConnectionString    = ConnectionString;
            this.PersistSecurityInfo = PersistSecurityInfo;
            this.Password            = Password ?? String.Empty;
            this.Username            = Username;

            EnableIntegratedSecurity = false;
        }

        public SQLConnectionCredentials( String ConnectionString, Boolean PersistSecurityInfo = true )
        {
            if( String.IsNullOrWhiteSpace( ConnectionString ) )
                throw new ArgumentOutOfRangeException( "SQL Connection String Cannot Be Null Or Whitespace." );

            this.ConnectionString    = ConnectionString;
            this.PersistSecurityInfo = PersistSecurityInfo;

            Password                 = String.Empty;
            Username                 = String.Empty;
            EnableIntegratedSecurity = true;
        }

        public String ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = ( value.EndsWith(";") ? value
                                                          : value + ";" )  +
                                    $"persist security info={PersistSecurityInfo.ToString( )};" +
                                    ( EnableIntegratedSecurity ? "Integrated Security=true;"
                                                               : $"User ID={Username};" +
                                                                 ( String.IsNullOrEmpty( Password ) ? String.Empty
                                                                                                    : $"Password={Password};"));
            }
        }

        public Boolean EnableIntegratedSecurity
        {
            get;
        }

        public String Password
        {
            get;
        }
        
        public Boolean PersistSecurityInfo
        {
            get;
        }

        public override String ToString( )
        {
            return ConnectionString;
        }

        public String Username
        {
            get;
        }
    }
}
