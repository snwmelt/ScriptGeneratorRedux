using ScriptGeneratorRedux.Models.Core.IO.Database.Enums;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface IDatabase : IManyToOne<IDatabaseServer>
    {
        String Name { get; }
        ETargetEnvironment TargetEnvironment { get; }
        EDatabaseType Type { get; }
    }
}
