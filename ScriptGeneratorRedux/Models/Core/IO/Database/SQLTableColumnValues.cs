using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLTableColumnValues : ISQLTableColumnValues
    {
        #region Private Variables
        List<dynamic> _Values;
        #endregion

        public SQLTableColumnValues( ISQLTableColumnKey ColumnKey, ICollection<dynamic> Values )
        {
            this.ColumnKey = ColumnKey;
            this.Values    = Values;
        }

        public SQLTableColumnValues( ISQLTableColumnKey ColumnKey )
        {
            this.ColumnKey = ColumnKey;

            _Values = new List<dynamic>( );
        }
        
        public ISQLTableColumnKey ColumnKey
        {
            get;
        }

        public IEnumerable<dynamic> Values
        {
            get
            {
                return _Values;
            }
            set
            {
                _Values = new List<dynamic>( value );
            }
        }

        public void AddValue( dynamic Value )
        {
            _Values.Add( Value );
        }
    }
}
