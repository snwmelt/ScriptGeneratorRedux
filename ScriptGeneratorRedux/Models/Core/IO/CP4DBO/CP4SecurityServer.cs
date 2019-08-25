using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO
{
    internal sealed class CP4SecurityServer : ICP4SecurityServer
    {
        public CP4SecurityServer( ISQLConnectionCredentials SQLConnectionCredentials, String Name, String SecurityDBName )
        {
            this.SQLConnectionCredentials = SQLConnectionCredentials ?? throw new ArgumentNullException( "SQLConnectionCredentials Cannot Be Null." );
            this.Name                     = Name ?? throw new ArgumentNullException( "Server Name Cannot Be Null." );
            
            SecurityDB = new SQLDatabase( SecurityDBName, this );
        }

        public ISQLDatabase SecurityDB
        {
            get;
        }

        public String Name
        {
            get;
        }

        public ISQLConnectionCredentials SQLConnectionCredentials
        {
            get;
        }
    }
}
