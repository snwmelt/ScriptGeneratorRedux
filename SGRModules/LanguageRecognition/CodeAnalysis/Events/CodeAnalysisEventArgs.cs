using SGRModules.LanguageRecognition.CodeAnalysis.Events.Interfaces;
using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;
using System;
using System.Collections.Generic;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Events
{
    public sealed class CodeAnalysisEventArgs : EventArgs, ICodeAnalysisEvent
    {
        public ICodeComponent Component
        {
            get;
        }

        public IEnumerable<Exception> Exceptions
        {
            get;
        }

        public CodeAnalysisEventArgs( ICodeComponent Component, IEnumerable<Exception> Exceptions = null )
        {
            this.Component  = Component;
            this.Exceptions = Exceptions;
        }
    }
}