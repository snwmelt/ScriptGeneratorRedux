using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataContext
    {
        void CopyToClipboard( FlowDocument currentDocument );
        void ExportToFile( FlowDocument FlowDocument );
        IEnumerable<String> GetEnvironmentNames( String ServerName );
        IEnumerable<String> GetSecurityDBNames( String ServerName );
        ICollection<String> GetServerNames( );
        IEnumerable<Int64> GetStudyIDs( String ServerName, String EnvironmentName, String SecurityDBName );
        void Initialise( );
        event EventHandler<ILoadingEventArgs> OnInitialised;
        event EventHandler<ILoadingEventArgs<IReadOnlyCollection<ISQLServer>>> OnServersLoaded;
        void RegisterServerDetailsProvider( ISQLServerProvider SQLServerProvider );
        void UpdateServersList( );
    }
}
