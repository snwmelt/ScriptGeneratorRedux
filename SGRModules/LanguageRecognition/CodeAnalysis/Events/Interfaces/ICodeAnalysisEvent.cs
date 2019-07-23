using SGRModules.Events.Interfaces;
using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Events.Interfaces
{
    public interface ICodeAnalysisEvent : IEvent
    {
        ICodeComponent Component { get; }
    }
}
