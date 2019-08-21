using ScriptGeneratorRedux.Models.Core.Events.Enums;
using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.Events
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
