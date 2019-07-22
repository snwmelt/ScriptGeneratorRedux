using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;

namespace SGRAntlrl.Helpers.Interfaces
{
    internal interface IResourceProvider<TLexer, TParser>  
        where TLexer : Lexer
        where TParser : Parser
    {
        TLexer Lexer { get; }
        TParser Parser { get; }
        void SetSourceText( String text );
        CommonTokenStream TokenStream { get; }
        void Walk( IParseTreeListener ParseTreeListener, IParseTree ParseTree );
    }
}
