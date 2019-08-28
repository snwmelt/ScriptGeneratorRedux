using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    // TODO Remove
    internal interface ICP4StudyServer : ISQLServer, IEnumerableDataSource<ICP4Study>, IEnumerableDataSource<ECP4DepoplymentEnvironment>
    {
        IReadOnlyCollection<ICP4Study> Studies { get; }
        IReadOnlyCollection<ECP4DepoplymentEnvironment> Environments { get; }
    }
}
