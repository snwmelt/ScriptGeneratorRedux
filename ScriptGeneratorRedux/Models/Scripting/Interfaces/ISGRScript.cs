using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Scripting.Interfaces
{
    internal interface ISGRScript : ISGRScriptElement
    {
        ICollection<ISGRScriptElement> Elements { get; }
    }
}
