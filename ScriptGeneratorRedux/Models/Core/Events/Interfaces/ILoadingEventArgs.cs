using ScriptGeneratorRedux.Models.Core.Events.Enums;
using System;

namespace ScriptGeneratorRedux.Models.Core.Events.Interfaces
{
    internal interface ILoadingEventArgs
    {
        Exception Exception { get; }
        ELoadingState State { get; }
    }
}
