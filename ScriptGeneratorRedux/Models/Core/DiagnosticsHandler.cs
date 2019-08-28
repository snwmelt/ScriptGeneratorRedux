using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.Interfaces;
using System.Diagnostics;

namespace ScriptGeneratorRedux.Models.Core
{
    internal sealed class DiagnosticsHandler : IDiagnosticsHandler
    {
        public void Log( ISGREventArgs EventArgs )
        {
            if( Debugger.IsAttached )
            {
                if( EventArgs.Exception != null )
                {
                    Debug.WriteLine( $"Exception _> {EventArgs.Exception}" );
                    Debug.WriteLine( $"Message   _> {EventArgs.Exception.Message}" );
                    Debug.WriteLine( $"Stack     _> {EventArgs.Exception.StackTrace}" );
                }
            }
        }
    }
}
