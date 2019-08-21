namespace ScriptGeneratorRedux.Models.Core.Events.Interfaces
{
    interface ILoadingEventArgs<T> : ILoadingEventArgs
    {
        T Payload { get; }
    }
}
