using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISchemaRestrictions
    {
        String[ ] ToArray( );

        Enum[ ] ApplicableTo { get; }
    }
}
