using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using System;
using ScriptGeneratorRedux.Models.Core.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core.Events
{
    internal class LoadingEventArgs : EventArgs, ILoadingEventArgs
    {
        public Exception Exception{ get; }

        public ELoadingState State{ get; }

        public LoadingEventArgs( ELoadingState State, Exception Exception = null )
        {
            this.State     = State;
            this.Exception = Exception;
        }

    }
}
