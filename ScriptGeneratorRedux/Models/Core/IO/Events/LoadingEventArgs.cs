using System;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System.Diagnostics;

namespace ScriptGeneratorRedux.Models.Core.IO.Events
{
    internal class LoadingEventArgs : EventArgs, ILoadingEventArgs
    {
        public Exception Exception{ get; }

        public ELoadingState State{ get; }

        public LoadingEventArgs( ELoadingState State, Exception Exception = null )
        {
            this.State     = State;
            this.Exception = Exception;

            Core.Diagnostics.Log( this );
        }
    }
}
