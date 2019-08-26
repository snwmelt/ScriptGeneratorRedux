using System.Collections.Generic;
using System;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4DatabaseServiceProvider
    {
        IEnumerable<KeyValuePair<ISQLTableKey, ISQLTable>> GetData( ISQLDatabase sQLDatabase );
        IEnumerable<ECP4DepoplymentEnvironment> GetEnvironments( IEnumerable<ISQLServer> dataContext );
        IEnumerable<ECP4DepoplymentEnvironment> GetEnvironments( ISQLServer dataContext );
        IEnumerable<ICP4Study> GetStudies( ISQLServer CP4Server );
        IEnumerable<long> GetStudyIDs( IEnumerable<ISQLServer> SecurityServers, ECP4DepoplymentEnvironment environment );
        IEnumerable<long> GetStudyIDs( ISQLServer SecurityServer, ECP4DepoplymentEnvironment environment );
        IEnumerable<long> GetStudyIDs( IEnumerable<ISQLServer> SecurityServers );
        IEnumerable<long> GetStudyIDs( ISQLServer SecurityServer );
    }
}
