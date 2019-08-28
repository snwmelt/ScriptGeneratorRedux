using System;
using System.Collections;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using System.Linq;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLTable : ISQLTable
    {
        #region Private Variables
        private EIOState                                                         _Status;
        private HashSet<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>> _Data;
        #endregion

        private void InvokeDataLoaded( ELoadingState LoadingState, Exception Exception = null )
        {
            OnDataLoaded?.Invoke( this, 
                                  new LoadingEventArgs<IEnumerable<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>>>( LoadingState, this, Exception ) );
        }

        private void InvokStatusChanged( EIOState EIOState, Exception Exception = null )
        {
            _Status = EIOState;
            OnStatusChanged?.Invoke( this, new IOStateChange( EIOState, Exception ) );
        }

        public SQLTable( String Name, ISQLDatabase Database )
        {
            if( String.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentOutOfRangeException( "Name Cannot Be Null Or Whitespace." );

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

        public IEnumerator<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>> GetEnumerator( )
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
                _Data = new HashSet<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>>( Core.CP4DatabaseService.GetData( this ) );

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

        public event EventHandler<ILoadingEventArgs<IEnumerable<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>>>> OnDataLoaded;
        public event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;

        public EIOState Status
        {
            get
            {
                return _Status;
            }

            private set
            {
                if( _Status != value )
                    InvokStatusChanged( value );

            }
        }

        public IEnumerable<ISQLTableColumnValues> Columns
        {
            get
            {
                return this.Select( x => x.Value );
            }
        }
    }
}
