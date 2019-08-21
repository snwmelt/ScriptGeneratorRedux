using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataSource<T>
    {
        event EventHandler<ILoadingEventArgs<T>> OnDataLoaded;
        event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;
        EDataSourceStatus Status { get; }
    }
}
