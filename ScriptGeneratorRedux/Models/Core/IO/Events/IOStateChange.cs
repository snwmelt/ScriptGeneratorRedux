using System;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.Events
{
    internal sealed class IOStateChange : EventArgs, IIOStateChangedEventArgs
    {
        public Exception Exception
        {
            get;
        }

        public EIOState State
        {
            get;
        }

        public IOStateChange( EIOState State, Exception Exception = null )
        {
            this.State     = State;
            this.Exception = Exception;

            Core.Diagnostics.Log( this );
        }
    }
}
