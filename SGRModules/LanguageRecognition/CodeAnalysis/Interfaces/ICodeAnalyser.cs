using SGRModules.LanguageRecognition.CodeAnalysis.Events;
using SGRModules.LanguageRecognition.Interfaces;
using System;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Interfaces
{
    public interface ICodeAnalyser : IDisposable
    {
        event EventHandler<CodeAnalysisEventArgs> CodeAnalysedEvent;
        void Analyse( String Text );
        ILanguage TargetLanguage { get; }
    }
}