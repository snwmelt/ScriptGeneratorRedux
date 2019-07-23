using SGRModules.LanguageRecognition.CodeAnalysis.Events.Interfaces;
using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Events
{
    public sealed class CodeAnalysisEventArgs : EventArgs, ICodeAnalysisEvent
    {
        public ICodeComponent Component
        {
            get;
        }

        public IEnumerable<_Exception> Exceptions
        {
            get;
        }

        public CodeAnalysisEventArgs( ICodeComponent Component, IEnumerable<_Exception> Exceptions = null )
        {
            this.Component  = Component;
            this.Exceptions = Exceptions;
        }
    }
}