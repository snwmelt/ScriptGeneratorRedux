using ScriptGeneratorRedux.Models.Core.Events.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces
{
    interface ILoadingEventArgs<T> : ILoadingEventArgs, ISGREventArgs
    {
        T Payload { get; }
    }
}
