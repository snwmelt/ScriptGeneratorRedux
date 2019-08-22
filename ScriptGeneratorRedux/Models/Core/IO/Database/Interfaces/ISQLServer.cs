using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    interface ISQLServer
    {
        String Name { get; }
        ISQLConnectionCredentials SQLConnectionCredentials { get; }
    }
}
