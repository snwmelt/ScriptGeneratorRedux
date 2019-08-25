using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
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
        IEnumerable<String> GetSecurityDBNames( String ServerName = null );
        ICollection<String> GetServerNames( );
        IEnumerable<Int64> GetStudyIDs( String ServerName, String EnvironmentName = null );
        void Initialise( );
        event EventHandler<ILoadingEventArgs> OnInitialised;
        event EventHandler<ILoadingEventArgs<IReadOnlyCollection<ISQLServer>>> OnServersLoaded;
        void RegisterServerDetailsProvider( ISQLServerProvider SQLServerProvider );
        void UpdateServersList( );
    }
}
