using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataSource<T>
    {
        void Initialise( );
        event EventHandler<ILoadingEventArgs<T>> OnDataLoaded;
        event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;
        EIOState Status { get; }
    }
}
