using SGRModules.LanguageRecognition.CodeAnalysis.Enums;
using System;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Interfaces
{
    public interface ICodeComponent
    {
        Enum ComponentType { get; }
        IComponentLocation Location { get; }
        EComponentState State { get; }
        String Text { get; }
    }
}