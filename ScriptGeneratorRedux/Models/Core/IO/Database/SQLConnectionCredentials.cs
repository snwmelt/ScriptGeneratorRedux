using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLConnectionCredentials : ISQLConnectionCredentials
    {
        #region Private Variables
        private String _ConnectionString;
        #endregion

        public SQLConnectionCredentials( String ConnectionString, String Username, String Password = null, Boolean PersistSecurityInfo = true ) : this ( ConnectionString )
        {
            if( String.IsNullOrWhiteSpace( Username ) )
                throw new ArgumentOutOfRangeException( "Username Cannot Be Null Or Whitespace." );
            
            this.PersistSecurityInfo = PersistSecurityInfo;
            this.Password            = Password ?? String.Empty;
            this.Username            = Username;

            EnableIntegratedSecurity = false;
        }

        public SQLConnectionCredentials( String ConnectionString, Boolean PersistSecurityInfo ) : this( ConnectionString )
        {
            this.PersistSecurityInfo = PersistSecurityInfo;
        }

        public SQLConnectionCredentials( String ConnectionString )
        {
            if( String.IsNullOrWhiteSpace( ConnectionString ) )
                throw new ArgumentOutOfRangeException( "SQL Connection String Cannot Be Null Or Whitespace." );

            this.ConnectionString = ConnectionString;

            Password                 = String.Empty;
            Username                 = String.Empty;
            EnableIntegratedSecurity = true;
            PersistSecurityInfo      = true;
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
