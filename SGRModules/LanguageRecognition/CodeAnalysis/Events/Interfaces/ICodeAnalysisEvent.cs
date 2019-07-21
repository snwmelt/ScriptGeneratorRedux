using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;
using System;
using System.Collections.Generic;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Events.Interfaces
{
    public interface ICodeAnalysisEvent
    {
        ICodeComponent Component { get; }
        IEnumerable<Exception> Exceptions { get; }
    }
}
