using SGRModules.Events.Interfaces;

namespace SGRModules.Factories.Events.Interfaces
{
    public interface IModuleInstantiatedEvent<T> : IEvent
    {
        T Instance { get; }
        string SourceDLL { get; }
    }
}