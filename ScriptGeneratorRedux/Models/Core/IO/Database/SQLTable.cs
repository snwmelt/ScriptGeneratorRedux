using System;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLTable : AIEnumerableDataSource<ISQLTableColumn>, ISQLTable
    {
        #region Private Variables
        private HashSet<ISQLTableColumn> _Data;
        #endregion

        public SQLTable( String Name, ISQLTableKey TableKey, ISQLDatabase Database ) : this( Name, Database )
        {
            if( TableKey == null )
                throw new ArgumentOutOfRangeException( "SQL Table TableKey Cannot Be Null Or Whitespace." );

            this.TableKey = TableKey;
        }

        public SQLTable( String Name, ISQLDatabase Database )
        {
            if( String.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentOutOfRangeException( "SQL Table Name Cannot Be Null Or Whitespace." );

            if( Database == null )
                throw new ArgumentOutOfRangeException( "SQL Table Database Cannot Be Null Or Whitespace." );

            this.Name     = Name;
            this.Database = Database;
            
            ConnectionCredentials = Database.ConnectionCredentials;
        }

        public ISQLConnectionCredentials ConnectionCredentials
        {
            get;
        }

        public ISQLDatabase Database
        {
            get;
        }

        public override IEnumerator<ISQLTableColumn> GetEnumerator( )
        {
            return _Data.GetEnumerator( );
        }

        public override void LoadData( )
        {
            try
            {
                _Data = new HashSet<ISQLTableColumn>( Core.CP4DatabaseService.GetData( this ) );

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

        public ISQLTableKey TableKey
        {
            get;
        }
    }
}
