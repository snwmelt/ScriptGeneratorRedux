using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ICP4DatabaseServiceProvider
    {
        IReadOnlyCollection<ICP4Study> GetStudies( ISQLServer Server );
    }
}
