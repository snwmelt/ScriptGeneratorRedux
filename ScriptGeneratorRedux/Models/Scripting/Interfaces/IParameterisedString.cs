using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Scripting.Interfaces
{
    internal interface IParameterisedString
    {
        String BaseValue { get; }
        ICollection<ISGRScriptParameter> Parameter { get; }
        void Parse( );
        String ParsedValue { get; }
    }
}