using System;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System.Collections;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO
{
    internal sealed class CP4StudyServer : ICP4StudyServer
    {
        #region EventHandler Backing Fields
        private event EventHandler<ILoadingEventArgs<IEnumerable<ICP4Study>>>                  _OnStudyDataLoaded;
        private event EventHandler<ILoadingEventArgs<IEnumerable<ECP4DepoplymentEnvironment>>> _OnEnvironmentDataLoaded;
        #endregion

        #region Thread Lock Objects
        private readonly Object _EnvironmentEventLock = new Object( );
        private readonly Object _StudyEventLock       = new Object( );
        #endregion

        private EIOState _Status;

        public CP4StudyServer( ISQLConnectionCredentials SQLConnectionCredentials, String Name = null )
        {
            this.SQLConnectionCredentials = SQLConnectionCredentials ?? throw new ArgumentOutOfRangeException( "Connection String Cannot Be Null, Empty, Or Whitespace." );
            this.Name                     = Name ?? SQLConnectionCredentials.ConnectionString;
        }

        public IReadOnlyCollection<ECP4DepoplymentEnvironment> Environments
        {
            get;
            private set;
        }

        public void LoadData( )
        {
            Studies = Core.CP4DatabaseService?.GetStudies( this );

            if( Studies?.Count > 0 )
            {
                InvokeStudiesLoaded( );

                HashSet<ECP4DepoplymentEnvironment> UniqueEnvironments = new HashSet<ECP4DepoplymentEnvironment>( );

                foreach( ICP4Study CP4Study in Studies )
                {
                    CP4Study.OnDataLoaded += ( se, ev ) =>
                    {
                        foreach( ECP4DepoplymentEnvironment Environment in ev.Payload )
                        {
                            UniqueEnvironments.Add( Environment );
                        }
                    };

                    CP4Study.LoadData( );
                }

                Environments = UniqueEnvironments;

                InvokeEnvironmentsLoaded( );
            }
        }

        public String Name
        {
            get;
        }
        
        public event EventHandler<ILoadingEventArgs<IEnumerable<ICP4Study>>> OnDataLoaded
        {
            add
            {
                lock ( _StudyEventLock )
                    _OnStudyDataLoaded += value;
            }

            remove
            {
                lock ( _StudyEventLock )
                    _OnStudyDataLoaded -= value;
            }
        }

        event EventHandler<ILoadingEventArgs<IEnumerable<ECP4DepoplymentEnvironment>>> IDataSource<IEnumerable<ECP4DepoplymentEnvironment>>.OnDataLoaded
        {
            add
            {
                lock ( _EnvironmentEventLock )
                    _OnEnvironmentDataLoaded += value;
            }

            remove
            {
                lock ( _EnvironmentEventLock )
                    _OnEnvironmentDataLoaded -= value;
            }
        }

        public event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;

        public ISQLConnectionCredentials SQLConnectionCredentials
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
                {
                    _Status = value;
                    InvokeStatusChanged( );
                }
            }
        }
        
        private void InvokeEnvironmentsLoaded( )
        {
            _OnEnvironmentDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ECP4DepoplymentEnvironment>>( ELoadingState.Completed, Environments ) );
        }

        private void InvokeEnvironmentsLoaded( ELoadingState State, Exception Exception, IReadOnlyCollection<ECP4DepoplymentEnvironment> Data = null )
        {
            _OnEnvironmentDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ECP4DepoplymentEnvironment>>( State, Data, Exception ) );
        }

        private void InvokeStudiesLoaded( )
        {
            _OnStudyDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ICP4Study>>( ELoadingState.Completed, Studies ) );
        }

        private void InvokeStatusChanged( )
        {
            OnStatusChanged?.Invoke( this, new IOStateChange( Status ) );
        }

        private void InvokeStatusChanged( EIOState State, Exception Exception )
        {
            _Status = State;

            OnStatusChanged?.Invoke( this, new IOStateChange( Status, Exception ) );
        }

        private void InvokeStudiesLoaded( ELoadingState State, Exception Exception, IReadOnlyCollection<ICP4Study> Data = null )
        {
            _OnStudyDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ICP4Study>>( State, Data, Exception ) );
        }

        public IEnumerator<ICP4Study> GetEnumerator( )
        {
            return Studies?.GetEnumerator( );
        }

        IEnumerator<ECP4DepoplymentEnvironment> IEnumerable<ECP4DepoplymentEnvironment>.GetEnumerator( )
        {
            return Environments?.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            yield return Studies?.GetEnumerator( );
            yield return Environments?.GetEnumerator( );
        }

        public IReadOnlyCollection<ICP4Study> Studies
        {
            get;
            private set;
        }
    }
}
