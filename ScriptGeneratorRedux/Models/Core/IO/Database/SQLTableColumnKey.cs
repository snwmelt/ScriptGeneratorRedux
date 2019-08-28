using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLTableColumnKey : ISQLTableColumnKey
    {
        public SQLTableColumnKey( String Name )
        {
            if( String.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentOutOfRangeException( $"TableColumnKey Name Cannot be Null or Whitespace." );

            this.Name = Name;
        }

        public String Name
        {
            get;
        }
    }
}
