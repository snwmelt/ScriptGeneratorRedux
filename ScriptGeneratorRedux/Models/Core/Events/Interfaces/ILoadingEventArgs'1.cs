using ScriptGeneratorRedux.Models.Core.Events.Enums;
using System.Runtime.InteropServices;

namespace ScriptGeneratorRedux.Models.Core.Events.Interfaces
{
    interface ILoadingEventArgs<T>
    {
        _Exception Exception { get; }
        T Payload { get; }
        ELoadingState State { get; }
    }
}
