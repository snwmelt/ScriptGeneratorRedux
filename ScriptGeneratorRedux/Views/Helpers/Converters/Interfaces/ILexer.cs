using System;
using System.Windows.Documents;

namespace ScriptGeneratorRedux.Views.Helpers.Converters.Interfaces
{
    internal interface ILexer
    {
        FlowDocument Parse( FlowDocument Document );
    }
}
