using System;
using System.Collections;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLDatabase
    {
        ISQLServer Server { get; }
        String Name { get; }
        IDictionary<ISQLTabelKey, IEnumerable<dynamic>> Data { get; }
    }
}
