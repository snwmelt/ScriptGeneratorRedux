using SGRAntlrl.Helpers.Interfaces;
using System;

namespace SGRAntlrl.Helpers
{
    internal sealed class ResourceFactory
    {
        internal static IResourceProvider<T1, T2> GetResourceProvider<T1, T2>( String InputText = "" )
            where T1 : Antlr4.Runtime.Lexer
            where T2 : Antlr4.Runtime.Parser
        {
            return new SGRAntlrResourceProvider<T1, T2>( );
        }
    }
}
