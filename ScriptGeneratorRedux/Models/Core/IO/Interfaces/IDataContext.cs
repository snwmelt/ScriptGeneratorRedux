using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataContext : IEnumerableDataSource<ISQLServer>
    {
        void CopyToClipboard( FlowDocument currentDocument );
        void ExportToFile( FlowDocument FlowDocument );
        IEnumerable<String> GetEnvironmentNames( String ServerName = null );
        IEnumerable<String> GetSecurityDBNames( String ServerName = null );
        IEnumerable<String> GetServerNames( );
        IEnumerable<int> GetStudyIDs( String ServerName, ECP4DepoplymentEnvironment EnvironmentName );
        IEnumerable<int> GetStudyIDs( String ServerName = null );
        void RegisterServerDetailsProvider( ISQLServerProvider SQLServerProvider );
        void UpdateServersList( );
    }
}
