using ScriptGeneratorRedux.Models.Core.Events.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.Interfaces
{
    internal interface IDiagnosticsHandler
    {
        void Log( ISGREventArgs EventArgs );
    }
}
