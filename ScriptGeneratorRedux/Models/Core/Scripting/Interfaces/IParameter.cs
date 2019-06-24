using System;
using System.Runtime.Serialization;

namespace ScriptGeneratorRedux.Models.Core.Scripting.Interfaces
{
    internal interface IParameter : ISerializable
    {
        String Target
        {
            get;
        }

        Type GetType
        {
            get;
        }

        Object Value
        {
            get;
        }
    }
}