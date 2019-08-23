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
    }
}
