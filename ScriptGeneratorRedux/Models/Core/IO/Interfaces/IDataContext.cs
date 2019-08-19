using System.Windows.Documents;
using ScriptGeneratorRedux.ViewModels;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataContext
    {
        void ExportToFile( FlowDocument FlowDocument );
        void CopyToClipboard( FlowDocument currentDocument );
        void RegisterServerDetailsProvider( IServerDetailsProvider IServerDetailsProvider );
        void UpdateServersList( );
    }
}
