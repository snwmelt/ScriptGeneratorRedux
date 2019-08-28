using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLServerInterop
    {
        ISQLConnectionManager ConnectionManager { get; }
        IEnumerable<String[ ]> ExecuteSQL( ISQLConnectionCredentials ConnectionCredentials, String Query );
        ISQLDatabase GetDatabaseData( ISQLConnectionCredentials ServerConnectionCredentials, String DatabaseName );
        IEnumerable<String> GetTableNames( ISQLDatabase SQLDatabase );
        IEnumerable<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>> GetTableData( ISQLTable SQLTable );
    }
}
