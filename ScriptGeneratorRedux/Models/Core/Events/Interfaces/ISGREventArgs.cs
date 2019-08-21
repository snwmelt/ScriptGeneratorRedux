using System;

namespace ScriptGeneratorRedux.Models.Core.Events.Interfaces
{
    internal interface ISGREventArgs
    {
        Exception Exception { get; }
    }
}
