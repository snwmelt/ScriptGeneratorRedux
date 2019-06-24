using System;
using System.Runtime.Serialization;

namespace ScriptGeneratorRedux.Models.Core.Scripting.Interfaces
{
    internal interface IValidationPSQL : IPSQL
    {
        Boolean Floating
        {
            get;
            set;
        }

        Int32 Index
        {
            get;
            set;
        }

        Boolean IsValid
        {
            get;
        }

        void Validate( );
    }
}