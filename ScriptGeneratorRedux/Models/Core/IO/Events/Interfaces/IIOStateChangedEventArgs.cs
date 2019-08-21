using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces
{
    internal interface IIOStateChangedEventArgs : ISGREventArgs
    {
        EIOState State { get; }
    }
}
