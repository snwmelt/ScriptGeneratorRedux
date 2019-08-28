using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLConnectionCredentials : ISQLConnectionCredentials
    {
        #region Private Variables
        private String _ConnectionString;
        #endregion

        public SQLConnectionCredentials( String ConnectionString, String Username, String Password = null, Boolean PersistSecurityInfo = true ) : this( ConnectionString )
        {
            if( String.IsNullOrWhiteSpace( Username ) )
                throw new ArgumentOutOfRangeException( "Username Cannot Be Null Or Whitespace." );

            this.PersistSecurityInfo = PersistSecurityInfo;
            this.Password = Password ?? String.Empty;
            this.Username = Username;

            EnableIntegratedSecurity = false;
        }

        public SQLConnectionCredentials( String ConnectionString, Boolean PersistSecurityInfo ) : this( ConnectionString )
        {
            this.PersistSecurityInfo = PersistSecurityInfo;
        }

        public SQLConnectionCredentials( ISQLConnectionCredentialsProvider CredentialsProvider, params String[ ] ConnectionStringAdditions ) : this( CredentialsProvider.ConnectionCredentials, ConnectionStringAdditions )
        {

        }

        public SQLConnectionCredentials( ISQLConnectionCredentials SQLConnectionCredentials, params String[ ] ConnectionStringAdditions )
        {
            String _BaseConnectionString = ( SQLConnectionCredentials.ConnectionString.EndsWith(";") ) ? SQLConnectionCredentials.ConnectionString.Substring( 0, ( SQLConnectionCredentials.ConnectionString.Length - 1 ) )
                                                                                                       : SQLConnectionCredentials.ConnectionString;

            ConnectionString = _BaseConnectionString;

            foreach( String Addition in ConnectionStringAdditions )
            {
                ConnectionString = String.Join( ";", ConnectionString, Addition );
            }
            
            Password                 = SQLConnectionCredentials.Password;
            Username                 = SQLConnectionCredentials.Username;
            EnableIntegratedSecurity = SQLConnectionCredentials.EnableIntegratedSecurity;
            PersistSecurityInfo      = SQLConnectionCredentials.PersistSecurityInfo;
        }


        public SQLConnectionCredentials( String ConnectionString )
        {
            if( String.IsNullOrWhiteSpace( ConnectionString ) )
                throw new ArgumentOutOfRangeException( "SQL Connection String Cannot Be Null Or Whitespace." );
            
            Password                 = String.Empty;
            Username                 = String.Empty;
            EnableIntegratedSecurity = true;
            PersistSecurityInfo      = true;

            this.ConnectionString = ConnectionString;
        }

        public String ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
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
            return ( ConnectionString.EndsWith( ";" ) ? ConnectionString
                                                      : ConnectionString + ";" ) +
                   $"persist security info={PersistSecurityInfo.ToString( )};"   +
                   ( EnableIntegratedSecurity ? $"Integrated Security={EnableIntegratedSecurity.ToString( )};"
                                              : $"User ID={Username};" +
                                                ( String.IsNullOrEmpty( Password ) ? String.Empty
                                                                                   : $"Password={Password};" ) );
        }

        public String Username
        {
            get;
        }
    }
}
