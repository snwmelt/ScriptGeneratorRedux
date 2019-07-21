using SGRModules.LanguageRecognition.CodeAnalysis.Events;
using System;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Interfaces
{
    public interface ICodeAnalyser : IDisposable
    {
        event EventHandler<CodeAnalysisEventArgs> CodeAnalysedEvent;
        void Analyse( String Text );
    }
}