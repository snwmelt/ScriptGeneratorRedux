using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLServer : ISQLServer
    {
        public SQLServer( String Name, ISQLConnectionCredentials SQLConnectionCredentials )
        {
            this.Name                     = Name;
            this.SQLConnectionCredentials = SQLConnectionCredentials;
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
