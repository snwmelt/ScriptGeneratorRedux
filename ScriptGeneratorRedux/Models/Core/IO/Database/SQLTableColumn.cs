using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLTableColumn : AIEnumerableDataSource<Object>, ISQLTableColumn
    {
        #region Private Variables
        List<Object>       _Data = new List<Object>( );
        ISQLTableColumnKey _ColumnKey;
        #endregion

        public SQLTableColumn( String Name, ISQLTableColumnKey ColumnKey, ISQLTable Table ) : this( Name, Table )
        {
            if( Table == null )
                throw new ArgumentOutOfRangeException( "SQL Table Column ColumnKey Cannot Be Null." );

            this.ColumnKey = ColumnKey;
        }

        public SQLTableColumn( String Name, ISQLTable Table )
        {
            if( String.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentOutOfRangeException( "SQL Table Column Name Cannot Be Null Or Whitespace." );

            if( Table == null )
                throw new ArgumentOutOfRangeException( "SQL Table Column Table Cannot Be Null." );

            this.Name  = Name;
            this.Table = Table;
        }

        public void AddValue( dynamic Value )
        {
            _Data.Add( Value );
        }

        // TODO: Expand to fetch more data about the colum schema and data type.
        public ISQLTableColumnKey ColumnKey
        {
            get
            {
                return _ColumnKey ?? new SQLTableColumnKey( Name );
            }
            private set
            {
                _ColumnKey = value;
            }
        }

        public override IEnumerator<Object> GetEnumerator( )
        {
            return _Data.GetEnumerator( );
        }

        public override void LoadData( )
        {
            try
            {
                _Data = new List<Object>( Core.CP4DatabaseService.GetData( this ) );

                Status = ( _Data?.Count > 0 ) ? EIOState.Populated
                                              : EIOState.Empty;

                InvokeDataLoaded( ELoadingState.Completed );
            }
            catch( Exception Ex )
            {
                Status = ( _Data?.Count > 0 ) ? EIOState.Fallback
                                              : EIOState.Empty;

                InvokeDataLoaded( ELoadingState.Failed, Ex );
            }
        }

        public String Name
        {
            get;
        }
        
        public ISQLTable Table
        {
            get;
        }
    }
}
