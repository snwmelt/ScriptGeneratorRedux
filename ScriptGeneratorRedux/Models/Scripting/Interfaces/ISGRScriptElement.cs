using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Scripting.Interfaces
{
    internal interface ISGRScriptElement
    {
        String Name { get; }
        ICollection<IParameterisedString> Components { get; }
        ICollection<ISRGScriptDependency> Dependencies { get; }
        String Description { get; }
        ICollection<ISGRScriptParameter> Parameters { get; }
    }
}