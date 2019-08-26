using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataSource
    {
        void LoadData( );
        EIOState Status { get; }
    }
}
