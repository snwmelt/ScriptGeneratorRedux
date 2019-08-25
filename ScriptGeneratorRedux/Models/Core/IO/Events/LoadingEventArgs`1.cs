using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Events
{
    internal sealed class LoadingEventArgs<T> : LoadingEventArgs, ILoadingEventArgs<T>
    {
        public T Payload{ get; }

        public LoadingEventArgs( ELoadingState State, T Payload = default(T), Exception Exception = null ) : base( State, Exception )
        {
            this.Payload = Payload;
        }
    }
}
