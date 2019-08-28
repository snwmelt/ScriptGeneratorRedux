using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO
{
    internal sealed class CP4DatabaseServiceProvider : ICP4DatabaseServiceProvider
    {
        ISQLServerInterop _ISQLServerInterop = new MSSQLServerInterop( );

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

        public IEnumerable<String> GetTableNames( ISQLDatabase SQLDatabase )
        {
            return _ISQLServerInterop.GetTableNames( SQLDatabase );
        }

        public IEnumerable<dynamic> GetData( ISQLTableColumn TableColumn )
        {
            throw new NotImplementedException( );
        }

        public IEnumerable<ISQLTableColumn> GetData( ISQLTable ISQLTable )
        {
            return _ISQLServerInterop.GetTableData( ISQLTable );
        }

        public IEnumerable<ISQLDatabase> GetData( ISQLServer SQLServer )
        {
            throw new NotImplementedException( );
        }

        public IEnumerable<ISQLTable> GetData( ISQLDatabase SQLDatabase )
        {
            throw new NotImplementedException( );
        }
    }
}
