using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class DatabaseServer : IDatabaseServer
    {
        public string ConnectionString
        {
            get;
        }

        public string Name
        {
            get;
        }

        public ICollection<IDatabase> Databases
        {
            get;
        }

        public ICollection<ICP4Study> Studies
        {
            get;
        }

        public IEnumerable<IDatabase> Colleciton
        {
            get;
        }

        IEnumerable<ICP4Study> IOneToMany<ICP4Study>.Colleciton
        {
            get;
        }

        public void Add( IDatabase Item )
        {
            throw new NotImplementedException( );
        }

        public void Add( ICP4Study Item )
        {
            throw new NotImplementedException( );
        }

        public void Remove( IDatabase Item )
        {
            throw new NotImplementedException( );
        }

        public void Remove( ICP4Study Item )
        {
            throw new NotImplementedException( );
        }
    }
}
