using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLTable : IEnumerableDataSource<ISQLTableColumn>, ISQLConnectionCredentialsProvider
    {
        ISQLDatabase Database { get; }
        String Name { get; }
        ISQLTableKey TableKey { get; }
    }
}
