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
                    Debug.WriteLine( $"EXCEPTION   _> \n\t\t {EventArgs.Exception}" );
                    Debug.WriteLine( $"MESSAGE     _> \n\t\t {EventArgs.Exception.Message}" );
                    Debug.WriteLine( $"STACK TRACE _> \n\t\t {EventArgs.Exception.StackTrace}" );
                }
            }
        }
    }
}
