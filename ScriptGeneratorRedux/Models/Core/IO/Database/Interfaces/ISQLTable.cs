using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLTable : IEnumerableDataSource<ISQLTableColumn>, ISQLConnectionCredentialsProvider
    {
        ISQLDatabase Database { get; }
        String Name { get; }
        ISQLTableKey TableKey { get; }
    }
}
