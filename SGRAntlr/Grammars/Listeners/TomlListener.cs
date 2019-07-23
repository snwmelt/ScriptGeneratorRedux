using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using SGRAntlrl.Grammars.Lexers;
using SGRAntlrl.Grammars.Parsers;
using SGRAntlrl.Helpers;
using SGRAntlrl.Helpers.Interfaces;
using SGRModules.LanguageRecognition.CodeAnalysis.Events;
using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;
using SGRModules.LanguageRecognition.Interfaces;

namespace SGRAntlrl.Grammars.Listeners
{
    public sealed class TomlListener : tomlParserBaseListener, ICodeAnalyser
    {
        private readonly IResourceProvider<tomlLexer, tomlParser> _IResourceProvider = ResourceFactory.GetResourceProvider<tomlLexer, tomlParser>( );

        public ILanguage TargetLanguage
        {
            get
            {
                return LanguageFactory.GetLanguageInfo( "toml" );
            }
        }

        public void Analyse( String Text )
        {
            _IResourceProvider.SetSourceText( TestToml );
            _IResourceProvider.Walk( this, _IResourceProvider.Parser.document( ) );
        }

        public event EventHandler<CodeAnalysisEventArgs> CodeAnalysedEvent;

        public void Dispose( )
        {
            throw new NotImplementedException( );
        }
    }
}
