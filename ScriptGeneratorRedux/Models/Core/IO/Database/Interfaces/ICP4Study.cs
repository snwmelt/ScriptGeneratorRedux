using ScriptGeneratorRedux.Models.Core.IO.Database.Enums;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ICP4Study : IOneToMany<IDatabase>
    {
        Int32 ID { get; }
        String Name { get; }
        IDictionary<Tuple<ETargetEnvironment, EDatabaseType>, IDatabase> Databases { get; }
    }
}