using SGRModules.Factories.Events.Interfaces;
using System;

namespace SGRModules.Factories.Interfaces
{
    public interface IModuleFactory<T>
    {
        void LoadModules( String DLLPath );
        event EventHandler<IModuleInstantiatedEvent<T>> OnModuleInstantiatedEvent;
    }
}