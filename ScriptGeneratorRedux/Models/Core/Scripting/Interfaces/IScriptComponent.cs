using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ScriptGeneratorRedux.Models.Core.Scripting.Interfaces
{
    internal interface IScriptComponent : ISerializable
    {
        IReadOnlyCollection<IValidationPSQL> PostExecutionChecks
        {
            get;
        }

        IReadOnlyCollection<IValidationPSQL> PreExecutionChecks
        {
            get;
        }

        IPSQL SQLString
        {
            get;
        }


    }
}
