using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface IDatabaseServer : IOneToMany<IDatabase>, IOneToMany<ICP4Study>
    {
        String Name { get; }
        ICollection<IDatabase> Databases { get; }
        ICollection<ICP4Study> Studies { get; }
    }
}
