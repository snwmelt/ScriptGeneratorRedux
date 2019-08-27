using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLConnectionCredentials
    {
        String ConnectionString { get; }
        String Password { get; }
        String Username { get; }
        Boolean EnableIntegratedSecurity { get; }
        Boolean PersistSecurityInfo { get; }
    }
}
