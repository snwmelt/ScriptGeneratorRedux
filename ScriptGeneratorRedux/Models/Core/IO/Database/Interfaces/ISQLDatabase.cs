using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLDatabase : IEnumerableDataSource<KeyValuePair<ISQLTableKey, ISQLTable>>, ISQLConnectionCredentialsProvider
    {
        ISQLServer Server { get; }
        String Name { get; }
        void LoadTable( String TableName );
    }
}