using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO
{
    internal sealed class SQLDatabase : ISQLDatabase
    {
        #region Private Variables
        private EIOState _Status;
        private HashSet<KeyValuePair<ISQLTableKey, ISQLTable>> _Data;
        #endregion

        private void InvokStatusChanged( EIOState EIOState, Exception Exception = null )
        {
            _Status = EIOState;
            OnStatusChanged?.Invoke( this, new IOStateChange( EIOState, Exception ) );
        }
        
        public SQLDatabase( String Name, ISQLServer Server )
        {
            this.Name   = Name ?? throw new ArgumentNullException( "Database Name Cannot Be Null." );
            this.Server = Server ?? throw new ArgumentNullException( "Server Cannot Be Null." );

            _Data = new HashSet<KeyValuePair<ISQLTableKey, ISQLTable>>( );
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
            throw new NotImplementedException( );
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
                    InvokStatusChanged( value );

            }
        }
    }
}