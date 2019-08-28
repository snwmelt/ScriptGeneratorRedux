﻿using System;
using System.Collections;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal abstract class AIEnumerableDataSource<T> : IEnumerableDataSource<T>
    {
        #region Protected Variables
        protected EIOState _Status;
        #endregion
        
        protected void InvokeDataLoaded( ELoadingState LoadingState, Exception Exception = null )
        {
            OnDataLoaded?.Invoke( this,
                                  new LoadingEventArgs<IEnumerable<T>>( LoadingState, this, Exception ) );
        }

        protected void InvokStatusChanged( EIOState EIOState, Exception Exception = null )
        {
            _Status = EIOState;
            OnStatusChanged?.Invoke( this, new IOStateChange( EIOState, Exception ) );
        }

        public EIOState Status
        {
            get
            {
                return _Status;
            }

            protected set
            {
                if( _Status != value )
                    InvokStatusChanged( value );

            }
        }

        public event EventHandler<ILoadingEventArgs<IEnumerable<T>>> OnDataLoaded;
        public event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;

        public abstract IEnumerator<T> GetEnumerator( );

        public abstract void LoadData( );

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }
    }
}
