using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLDatabase : IEnumerableDataSource<ISQLTable>, ISQLConnectionCredentialsProvider
    {
        ISQLServer Server { get; }
        String Name { get; }
        void LoadTable( String TableName );
    }
}