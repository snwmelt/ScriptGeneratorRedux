using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataSource<T> : IDataSource
    {
        event EventHandler<ILoadingEventArgs<T>> OnDataLoaded;
        event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;
    }
}
