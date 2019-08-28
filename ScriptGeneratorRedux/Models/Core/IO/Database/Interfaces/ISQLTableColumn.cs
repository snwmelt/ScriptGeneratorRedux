using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLTableColumn : IEnumerableDataSource<Object>
    {
        void AddValue( dynamic Value );
        ISQLTableColumnKey ColumnKey { get; }
        String Name { get; }
        ISQLTable Table { get; }
    }
}
