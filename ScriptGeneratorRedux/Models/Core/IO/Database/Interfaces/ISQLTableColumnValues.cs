using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLTableColumnValues
    {
        ISQLTableColumnKey ColumnKey { get; }
        IEnumerable<dynamic> Values { get; }
        void AddValue( dynamic Value );
    }
}
