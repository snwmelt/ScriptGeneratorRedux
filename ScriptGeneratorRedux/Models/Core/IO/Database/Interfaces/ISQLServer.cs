using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    interface ISQLServer : ISQLConnectionCredentialsProvider
    {
        String Name { get; }
    }
}
