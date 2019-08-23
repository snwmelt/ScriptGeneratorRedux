using System.Collections;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Interfaces
{
    internal interface IEnumerableDataSource<T> : IDataSource<IEnumerable<T>>, IEnumerable<T>, IEnumerable
    {
    }
}
