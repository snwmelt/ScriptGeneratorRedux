using System.Collections.Generic;
using System;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4DatabaseServiceProvider
    {
        IReadOnlyCollection<ICP4Study> GetStudies( ISQLServer Server );
        IEnumerable<ECP4DepoplymentEnvironment> GetEnvironments( ICP4StudyServer iCP4StudyServer );
        IEnumerable<ICP4Study> GetStudyIDs( String serverName, String EnvironmentName = null );
        IEnumerable<long> GetStudyIDs( IEnumerable<ICP4SecurityServer> SecurityServers );
        IEnumerable<long> GetStudyIDs( ICP4SecurityServer SecurityServer );
        IEnumerable<long> GetStudyIDs( IEnumerable<ICP4SecurityServer> SecurityServers, ECP4DepoplymentEnvironment Environment );
        IEnumerable<long> GetStudyIDs( ICP4SecurityServer SecurityServer, ECP4DepoplymentEnvironment Environment );
    }
}
