using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4StudyServer : ISQLServer, IDataSource<IReadOnlyCollection<ICP4Study>>, IDataSource<IReadOnlyCollection<ECP4DepoplymentEnvironment>>
    {
        IReadOnlyCollection<ICP4Study> Studies { get; }
        IReadOnlyCollection<ECP4DepoplymentEnvironment> Environments { get; }
    }
}
