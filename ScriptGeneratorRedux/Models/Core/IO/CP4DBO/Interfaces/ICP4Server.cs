using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4Server : IDataSource<IReadOnlyCollection<ICP4Study>>, IDataSource<IReadOnlyCollection<ECP4DepoplymentEnvironment>>
    {
        String ConnectionString { get; }
        String Name { get; }
        IReadOnlyCollection<ICP4Study> Studies { get; }
        IReadOnlyCollection<ECP4DepoplymentEnvironment> Environments { get; }
    }
}
