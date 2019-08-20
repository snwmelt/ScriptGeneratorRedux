using ScriptGeneratorRedux.Models.Core.Events.Enums;
using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace ScriptGeneratorRedux.Models.Core.IO.Events
{
    internal sealed class DataLoadedEventArgs<T> : EventArgs, ILoadingEventArgs<T>
    {
        public _Exception Exception{ get; }

        public T Payload{ get; }

        public ELoadingState State{ get; }

        public DataLoadedEventArgs( ELoadingState State, T Payload = default(T), _Exception Exception = null )
        {
            this.State     = State;
            this.Payload   = Payload;
            this.Exception = Exception;
        }
    }
}
