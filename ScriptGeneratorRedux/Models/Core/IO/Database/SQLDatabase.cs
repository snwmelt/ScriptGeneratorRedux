using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO
{
    internal sealed class SQLDatabase : ISQLDatabase
    {
        public SQLDatabase( String Name, ISQLServer Server )
        {
            this.Name   = Name ?? throw new ArgumentNullException( "Database Name Cannot Be Null." );
            this.Server = Server ?? throw new ArgumentNullException( "Server Cannot Be Null." );
        }

        public ISQLServer Server
        {
            get;
        }

        public String Name
        {
            get;
        }

        public IDictionary<ISQLTabelKey, IEnumerable<dynamic>> Data
        {
            get;
        }
    }
}