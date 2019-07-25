using SGRModules.LanguageRecognition.CodeAnalysis.Enums;
using System;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Interfaces
{
    public interface ICodeComponent
    {
        ECodeComponentType ComponentType { get; }
        EComponentState State { get; }
        IComponentLocation Location { get; }
        String Text { get; }
    }
}