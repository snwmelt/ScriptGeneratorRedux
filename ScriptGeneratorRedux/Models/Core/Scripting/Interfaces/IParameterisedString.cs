using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ScriptGeneratorRedux.Models.Core.Scripting.Interfaces
{
    internal interface IParameterisedString : ISerializable
    {
        String Output
        {
            get;
        }

        IReadOnlyCollection<IParameter> Parameters
        {
            get;
        }

        void Populate( String Template, params IParameter[ ] Parameters );

        String Template
        {
            get;
        }
    }
}