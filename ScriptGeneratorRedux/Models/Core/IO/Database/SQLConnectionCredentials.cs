using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLConnectionCredentials : ISQLConnectionCredentials
    {
        public SQLConnectionCredentials( String ConnectionString, Boolean UseWindowsAuthenication, String Username = null, String Password = null )
        {
            this.ConnectionString         = ConnectionString;
            this.Password                 = Password;
            this.Username                 = Username;
            this.UseWindowsAuthentication = UseWindowsAuthentication;
        }

        public String ConnectionString
        {
            get;
        }

        public String Password
        {
            get;
        }

        public String Username
        {
            get;
        }

        public Boolean UseWindowsAuthentication
        {
            get;
        }
    }
}
