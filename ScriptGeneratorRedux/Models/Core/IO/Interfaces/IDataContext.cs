using System.Windows.Documents;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IDataContext
    {
        void ExportToFile( FlowDocument FlowDocument );
        void CopyToClipboard( FlowDocument currentDocument );
    }
}
