using System;

namespace ScriptGeneratorRedux.Models.Scripting.Interfaces
{
    internal interface ISRGScriptDependency
    {
        void Check( );
        Boolean IsSatisfied { get; }
        String Name { get; }
        Type Type { get; }
        Object Value { get; }
    }
}