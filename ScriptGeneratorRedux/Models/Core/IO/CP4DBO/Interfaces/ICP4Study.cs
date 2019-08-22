using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4Study : IDataSource<IEnumerable<ECP4DepoplymentEnvironment>>
    {
    }
}