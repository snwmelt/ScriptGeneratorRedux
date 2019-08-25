using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLDatabase : IEnumerableDataSource<KeyValuePair<ISQLTableKey, ISQLTable>>
    {
        ISQLServer Server { get; }
        String Name { get; }
    }
}