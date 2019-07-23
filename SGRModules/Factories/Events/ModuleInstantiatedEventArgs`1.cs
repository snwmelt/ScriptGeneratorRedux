using SGRModules.Factories.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SGRModules.Factories
{
    internal class ModuleInstantiatedEventArgs<T> : EventArgs, IModuleInstantiatedEvent<T>
    {
        public T Instance
        {
            get;
        }

        public String SourceDLL
        {
            get;
        }

        public IEnumerable<_Exception> Exceptions
        {
            get;
        }

        public ModuleInstantiatedEventArgs( T Instance, String SourceDLL, IEnumerable<_Exception> Exceptions = null )
        {
            this.Instance   = Instance;
            this.SourceDLL  = SourceDLL;
            this.Exceptions = Exceptions;
        }
    }
}