using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLDatabase : AIEnumerableDataSource<ISQLTable>, ISQLDatabase
    {
        #region Private Variables
        private HashSet<ISQLTable> _Data = new HashSet<ISQLTable>( );
        #endregion
        
        private ISQLTable _LoadTable( String TableName )
        {
            ISQLTable ISQLTable = new SQLTable( TableName, this );

            ISQLTable.LoadData( );

            return ISQLTable;
        }

        public SQLDatabase( String Name, ISQLServer Server )
        {
            if( String.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentOutOfRangeException( "Database Name Cannot Be Null or Whitespace." );

            if ( Server == null )
                throw new ArgumentNullException( "Server Cannot Be Null." );

            this.Name   = Name;
            this.Server = Server;

            ConnectionCredentials = new SQLConnectionCredentials( Server, $"Database={Name}" );
        }

        public ISQLConnectionCredentials ConnectionCredentials
        {
            get;
        }

        public override IEnumerator<ISQLTable> GetEnumerator( )
        {
            return _Data.GetEnumerator( );
        }

        public override void LoadData( )
        {
            try
            {
                if( _Data.Count > 0 )
                    _Data.Clear( );

                _Data = new HashSet<ISQLTable>( );

                foreach( String TableName in Core.CP4DatabaseService.GetTableNames( this ) )
                {
                    _Data.Add( _LoadTable( TableName ) );
                }

                Status = ( _Data?.Count > 0 ) ? EIOState.Populated
                                              : EIOState.Empty;

                InvokeDataLoaded( ELoadingState.Completed );
            }
            catch ( Exception Ex )
            {
                Status = ( _Data?.Count > 0 ) ? EIOState.Fallback
                                              : EIOState.Empty;

                InvokeDataLoaded( ELoadingState.Failed, Ex );
            }
        }

        public void LoadTable( String TableName )
        {
            try
            {
                ISQLTable Table = _LoadTable( TableName );

                if ( _Data.Contains( Table ) )
                    _Data.Remove( Table );

                _Data.Add( Table );

                Status = EIOState.Populated;

                if ( Table.Status != EIOState.Populated )
                {
                    InvokeDataLoaded( ELoadingState.Failed );
                }
                else
                {
                    InvokeDataLoaded( ELoadingState.Completed );
                }
            }
            catch( Exception Ex )
            {
                Status = ( _Data?.Count > 0 ) ? EIOState.Fallback
                                              : EIOState.Empty;

                InvokeDataLoaded( ELoadingState.Failed, Ex );
            }
        }

        public string Name
        {
            get;
        }
        
        public ISQLServer Server
        {
            get;
        }
    }
}