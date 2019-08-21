using System;
using System.Collections.Generic;
using System.Windows.Documents;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.ViewModels;

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
        event EventHandler<LoadingEventArgs> OnInitialised;
        event EventHandler<LoadingEventArgs<ICollection<String>>> OnServersLoaded;
        void RegisterServerDetailsProvider( IDatabaseServerProvider IServerDetailsProvider );
        void UpdateServersList( );
    }
}
