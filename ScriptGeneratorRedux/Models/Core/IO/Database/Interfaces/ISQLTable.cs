using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLTable : IEnumerableDataSource<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValue>>
    {
    }
}
