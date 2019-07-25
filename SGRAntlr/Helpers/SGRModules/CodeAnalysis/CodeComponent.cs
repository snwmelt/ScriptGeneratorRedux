using System;
using SGRModules.LanguageRecognition.CodeAnalysis.Enums;
using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;

namespace SGRAntlrl.Helpers.SGRModules.CodeAnalysis
{
    internal class CodeComponent : ICodeComponent
    {
        public Enum ComponentType
        {
            get;
            internal set;
        }

        public IComponentLocation Location
        {
            get;
            internal set;
        }

        public EComponentState State
        {
            get;
            internal set;
        }

        public String Text
        {
            get;
            internal set;
        }
    }
}