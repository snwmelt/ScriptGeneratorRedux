using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface IOneToMany<T>
    {
        void Add( T Item );
        void Remove( T Item );
        IEnumerable<T> Colleciton { get; }
    }
}
