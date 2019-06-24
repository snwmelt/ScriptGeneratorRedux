using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ScriptGeneratorRedux.Models.Core.Scripting.Interfaces
{
    internal interface IPSQL : IParameterisedString
    {
        Boolean Validate( );
    }
}