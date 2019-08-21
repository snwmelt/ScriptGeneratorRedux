namespace ScriptGeneratorRedux.Models.Core.Events.Interfaces
{
    interface ILoadingEventArgs<T> : ILoadingEventArgs, ISGREventArgs
    {
        T Payload { get; }
    }
}
