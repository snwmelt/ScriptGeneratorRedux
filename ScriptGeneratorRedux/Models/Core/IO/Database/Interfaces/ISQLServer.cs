using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    interface ISQLServer : IEnumerableDataSource<ISQLDatabase>, ISQLConnectionCredentialsProvider
    {
        String Name { get; }
    }
}
