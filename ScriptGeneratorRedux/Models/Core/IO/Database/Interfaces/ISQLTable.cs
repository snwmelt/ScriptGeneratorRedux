using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLTable : IEnumerableDataSource<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>>, ISQLConnectionCredentialsProvider
    {
        ISQLDatabase Database { get; }
        String Name { get; }
        IEnumerable<ISQLTableColumnValues> Columns { get; }
    }
}
