using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO
{
    internal sealed class CP4DatabaseServiceProvider : ICP4DatabaseServiceProvider
    {
        public IEnumerable<KeyValuePair<ISQLTableKey, ISQLTable>> GetData( ISQLDatabase Database )
        {
            return null;
        }

        public IEnumerable<ECP4DepoplymentEnvironment> GetEnvironments( IEnumerable<ISQLServer> Servers )
        {
            return null;
        }

        public IEnumerable<ECP4DepoplymentEnvironment> GetEnvironments( ISQLServer Server )
        {
            return null;
        }

        public IEnumerable<ICP4Study> GetStudies( ISQLServer Server )
        {
            return null;
        }

        public IEnumerable<long> GetStudyIDs( IEnumerable<ISQLServer> Servers, ECP4DepoplymentEnvironment Environment )
        {
            return null;
        }

        public IEnumerable<long> GetStudyIDs( ISQLServer Server, ECP4DepoplymentEnvironment Environment )
        {
            return null;
        }

        public IEnumerable<long> GetStudyIDs( IEnumerable<ISQLServer> Servers )
        {
            return null;
        }

        public IEnumerable<long> GetStudyIDs( ISQLServer Server )
        {
            return null;
        }
    }
}
