using ScriptGeneratorRedux.Models.Core.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core.Events.Interfaces
{
    internal interface ILoadingEventArgs : ISGREventArgs
    {
        ELoadingState State { get; }
    }
}
