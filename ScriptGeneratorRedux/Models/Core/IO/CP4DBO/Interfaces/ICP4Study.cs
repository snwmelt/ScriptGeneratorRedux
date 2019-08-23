using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4Study : IEnumerableDataSource<ECP4DepoplymentEnvironment>
    {
        Int64 ID { get; }
    }
}