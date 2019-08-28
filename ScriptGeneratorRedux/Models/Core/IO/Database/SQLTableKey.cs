using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLTableKey : ISQLTableKey
    {
        public SQLTableKey( String Name )
        {
            this.Name = Name;
        }

        public String Name
        {
            get;
        }
    }
}
