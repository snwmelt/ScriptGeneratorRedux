using System.Collections.Generic;
using System;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4DatabaseServiceProvider
    {
        IEnumerable<KeyValuePair<ISQLTableKey, ISQLTable>> GetData( ISQLDatabase Database );
        IEnumerable<ECP4DepoplymentEnvironment> GetEnvironments( IEnumerable<ISQLServer> Servers );
        IEnumerable<ECP4DepoplymentEnvironment> GetEnvironments( ISQLServer Server );
        IEnumerable<ICP4Study> GetStudies( ISQLServer Server );
        IEnumerable<long> GetStudyIDs( IEnumerable<ISQLServer> Servers, ECP4DepoplymentEnvironment Environment );
        IEnumerable<long> GetStudyIDs( ISQLServer Server, ECP4DepoplymentEnvironment Environment );
        IEnumerable<long> GetStudyIDs( IEnumerable<ISQLServer> Servers );
        IEnumerable<long> GetStudyIDs( ISQLServer Server );
    }
}
