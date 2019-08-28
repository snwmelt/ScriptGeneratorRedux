using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLDatabase : ISQLDatabase
    {
        #region Private Variables
        private EIOState                                       _Status;
        private HashSet<KeyValuePair<ISQLTableKey, ISQLTable>> _Data   = new HashSet<KeyValuePair<ISQLTableKey, ISQLTable>>( );
        #endregion

        private void _InvokeDataLoaded( ELoadingState LoadingState, Exception Exception = null )
        {
            OnDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<KeyValuePair<ISQLTableKey, ISQLTable>>>( LoadingState, this, Exception ) );
        }

        private void _InvokeStatusChanged( EIOState EIOState, Exception Exception = null )
        {
            _Status = EIOState;
            OnStatusChanged?.Invoke( this, new IOStateChange( EIOState, Exception ) );
        }

        private KeyValuePair<ISQLTableKey, ISQLTable> _LoadTable( String TableName )
        {
            ISQLTable    ISQLTable    = new SQLTable( TableName, this );
            ISQLTableKey ISQLTableKey = new SQLTableKey( TableName );

            ISQLTable.LoadData( );

            return new KeyValuePair<ISQLTableKey, ISQLTable>( ISQLTableKey, ISQLTable );
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

        public IEnumerator<KeyValuePair<ISQLTableKey, ISQLTable>> GetEnumerator( )
        {
            return _Data.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        public void LoadData( )
        {
            try
            {
                if( _Data.Count > 0 )
                    _Data.Clear( );

                _Data = new HashSet<KeyValuePair<ISQLTableKey, ISQLTable>>( );

                foreach( String TableName in Core.CP4DatabaseService.GetTableNames( this ) )
                {
                    _Data.Add( _LoadTable( TableName ) );
                }

                Status = ( _Data?.Count > 0 ) ? EIOState.Populated
                                              : EIOState.Empty;

                _InvokeDataLoaded( ELoadingState.Completed );
            }
            catch ( Exception Ex )
            {
                Status = ( _Data?.Count > 0 ) ? EIOState.Fallback
                                              : EIOState.Empty;

                _InvokeDataLoaded( ELoadingState.Failed, Ex );
            }
        }

        public void LoadTable( String TableName )
        {
            try
            {
                KeyValuePair<ISQLTableKey, ISQLTable> Table = _LoadTable( TableName );

                if( _Data.Contains( Table ) )
                    _Data.Remove( Table );

                _Data.Add( Table );

                Status = ( _Data?.Count > 0 ) ? EIOState.Populated
                                              : EIOState.Empty;

                _InvokeDataLoaded( ELoadingState.Completed );
            }
            catch( Exception Ex )
            {
                Status = ( _Data?.Count > 0 ) ? EIOState.Fallback
                                              : EIOState.Empty;

                _InvokeDataLoaded( ELoadingState.Failed, Ex );
            }
            
            
        }

        public string Name
        {
            get;
        }

        public event EventHandler<ILoadingEventArgs<IEnumerable<KeyValuePair<ISQLTableKey, ISQLTable>>>> OnDataLoaded;
        public event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;

        public ISQLServer Server
        {
            get;
        }

        public EIOState Status
        {
            get
            {
                return _Status;
            }

            private set
            {
                if( _Status != value )
                    _InvokeStatusChanged( value );

            }
        }
    }
}