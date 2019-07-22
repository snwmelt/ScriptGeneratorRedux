using Antlr4.Runtime.Tree;
using SGRAntlrl.Helpers.Interfaces;
using System;

namespace SGRAntlrl.Helpers
{
    internal sealed class SGRAntlrResourceProvider<T1, T2> : IResourceProvider<T1, T2>
        where T1 : Antlr4.Runtime.Lexer
        where T2 : Antlr4.Runtime.Parser
    {
        public T1 Lexer
        {
            get;
            private set;
        }

        public T2 Parser
        {
            get;
            private set;
        }

        public CommonTokenStream TokenStream
        {
            get;
            private set;
        }

        public void SetSourceText( String Text )
        {
            Lexer       = ( T1 )Activator.CreateInstance( typeof( T1 ), new AntlrInputStream( Text ) );
            TokenStream = new CommonTokenStream( Lexer );
            Parser      = ( T2 )Activator.CreateInstance( typeof( T2 ), TokenStream );
        }

        public void Walk( IParseTreeListener ParseTreeListener, IParseTree ParseTree )
        {
            ( new ParseTreeWalker( ) ).Walk( ParseTreeListener, ParseTree );
        }
    }
}