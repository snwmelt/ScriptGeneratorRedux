using System;
using ScriptGeneratorRedux.Models.Core.Authentication.Interfaces;
using ScriptGeneratorRedux.Models.Extensions;

namespace ScriptGeneratorRedux.Models.Core.Authentication
{
    internal sealed class DummyAuthManager : IUserAuthenticator
    {
        public Boolean HasAuthenticatedUser
        {
            get
            {
                return !( UserID == null ) &&
                       !( UserKey == null ) &&
                       !( Username == null );
            }
        }

        public String UserID
        {
            get;
            private set;
        }

        public Byte[ ] UserKey
        {
            get;
            private set;
        }

        public String Username
        {
            get;
            private set;
        }

        public void Authenticate( String Username, String Password )
        {
            this.Username = Username;
            this.UserKey  = Password.GetSHA256Hash( ).Generate256BitKey( "DummySalt" );
            this.UserID   = "0";
        }

        public Boolean Create( String Username, String Password )
        {
            Authenticate( Username, Password );

            return true;
        }

        public void DeAuthenticate( )
        {
            UserID   = null;
            UserKey  = null;
            Username = null;
        }

        public Boolean IsAValidPassword( String Password )
        {
            return !String.IsNullOrEmpty( Password );
        }

        public Boolean IsAValidUsername( String Username )
        {
            return !String.IsNullOrEmpty( Username );
        }

        public Boolean UserExists( String username )
        {
            return !String.IsNullOrEmpty( Username );
        }
    }
}
