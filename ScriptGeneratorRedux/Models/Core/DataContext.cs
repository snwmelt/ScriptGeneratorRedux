using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using ScriptGeneratorRedux.Models.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ScriptGeneratorRedux.Models.Core
{
    internal class DataContext : IDataContext
    {
        #region Private Variables
        HashSet<ICP4SecurityServer>  _ICP4SecurityServers;
        readonly Object              _ICP4SecurityServersLock;
        HashSet<ISQLServerProvider>  _SQLServerProviders;
        EIOState                     _Status;
        #endregion

        private IEnumerable<ISQLTableColumn> _GetStudyIDColumns( )
        {
            return _GetStudyTables( )?.SelectMany( x => x.Where( y => y.Name == "StudyID" ) );
        }

        private IEnumerable<int> _GetStudyIDs( String ServerName = null, IEnumerable<ISQLTableColumn> StudyIDColumns = null )
        {
            StudyIDColumns = StudyIDColumns ?? _GetStudyIDColumns( );

            return ( String.IsNullOrWhiteSpace( ServerName ) ) ? StudyIDColumns?.SelectMany( x => x.Select( y => int.Parse( y.ToString( ) ) ) )
                                                               : StudyIDColumns?.Where( x => x.Table.Database.Server.Name == ServerName )
                                                                               ?.SelectMany( x => x.Select( y => int.Parse( y.ToString( ) ) ) );
        }

        private IEnumerable<ISQLTable> _GetStudyTables( )
        {
            return _ICP4SecurityServers?.SelectMany( x => x.SecurityDB )
                                       ?.Where( x => x.Name == "Studies" );
        }

        private void _InvokeDataLoaded( ELoadingState LoadingState, Exception Exception = null )
        {
            OnDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ISQLServer>>( LoadingState, this, Exception ) );
        }

        private void _InvokStatusChanged( EIOState EIOState, Exception Exception = null )
        {
            _Status = EIOState;
            OnStatusChanged?.Invoke( this, new IOStateChange( EIOState, Exception ) );
        }

        private void _OnServerProviderDataLoaded( Object Sender, ILoadingEventArgs<IEnumerable<ISQLServer>> EventArgs )
        {
            foreach( ISQLServer ISQLServer in EventArgs.Payload )
            {
                if( ISQLServer is ICP4SecurityServer )
                {
                    ICP4SecurityServer ICP4SecurityServer = ISQLServer as ICP4SecurityServer;

                    ICP4SecurityServer.SecurityDB?.LoadTable( "Studies" );

                    lock( _ICP4SecurityServersLock )
                    {
                        if( _ICP4SecurityServers.Contains( ICP4SecurityServer ) )
                            _ICP4SecurityServers.Remove( ICP4SecurityServer );

                        _ICP4SecurityServers.Add( ICP4SecurityServer );
                    }
                }
            }
        }

        private void _ProcessSQLServerProviders( )
        {
            Parallel.ForEach( _SQLServerProviders,
                              ( ISQLServerProvider ) =>
                              {
                                  try
                                  {
                                      ISQLServerProvider.LoadData( );
                                  }
                                  catch ( Exception Ex )
                                  {
                                      _InvokeDataLoaded( ELoadingState.PartialError, Ex );
                                  }
                              } );
        }


        public DataContext( )
        {
            _ICP4SecurityServers     = new HashSet<ICP4SecurityServer>( );
            _ICP4SecurityServersLock = new Object( );
            _SQLServerProviders      = new HashSet<ISQLServerProvider>( );
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
                    _InvokStatusChanged( value );

            }
        }

        public event EventHandler<ILoadingEventArgs<IEnumerable<ISQLServer>>> OnDataLoaded;
        public event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;

        public void CopyToClipboard( FlowDocument currentDocument )
        {
            throw new NotImplementedException( );
        }

        public void ExportToFile( FlowDocument FlowDocument )
        {
            throw new NotImplementedException( );
        }
        
        public IEnumerable<String> GetEnvironmentNames( String ServerName = null )
        {
            IEnumerable<ISQLTable> StudyTables = ( ServerName == null ) ? _GetStudyTables( )
                                                                        : _GetStudyTables( )?.Where( x => x.Database.Server.Name == ServerName );

            if ( StudyTables.Any( t => t.Any( c => c.Name == "BuildVersionID" && c.Any( v => double.Parse( v?.ToString( ) ) > 0 ) ) ) )
                yield return ECP4DepoplymentEnvironment.Build.GetDescription( );

            if ( StudyTables.Any( t => t.Any( c => c.Name == "LiveVersionID" && c.Any( v => double.Parse( v?.ToString( ) ) > 0 ) ) ) )
                yield return ECP4DepoplymentEnvironment.Live.GetDescription( );

            if ( StudyTables.Any( t => t.Any( c => c.Name == "TestVersionID" && c.Any( v => double.Parse( v?.ToString( ) ) > 0 ) ) ) )
                yield return ECP4DepoplymentEnvironment.Test.GetDescription( );

            if ( StudyTables.Any( t => t.Any( c => c.Name == "UATVersionID" && c.Any( v => double.Parse( v?.ToString( ) ) > 0 ) ) ) )
                yield return ECP4DepoplymentEnvironment.UAT.GetDescription( );
        }

        public IEnumerator<ISQLServer> GetEnumerator( )
        {
            foreach( ICP4SecurityServer ICP4SecurityServer in _ICP4SecurityServers )
                yield return ICP4SecurityServer as ISQLServer;
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        public IEnumerable<String> GetSecurityDBNames( String ServerName = null )
        {
            return ( String.IsNullOrWhiteSpace( ServerName ) ) ? _ICP4SecurityServers?.Select( x => x.SecurityDB.Name )
                                                               : _ICP4SecurityServers?.Where( x => x.Name == ServerName )
                                                                                     ?.Select( x => x.SecurityDB.Name );
        }

        public IEnumerable<String> GetServerNames( )
        {
            return this.Select( x => x.Name );
        }


        public IEnumerable<int> GetStudyIDs( ECP4DepoplymentEnvironment Environment )
        {
            return GetStudyIDs( null, Environment );
        }

        public IEnumerable<int> GetStudyIDs( String ServerName, ECP4DepoplymentEnvironment Environment )
        {
            return null;
        }

        public IEnumerable<int> GetStudyIDs( String ServerName = null )
        {
            return _GetStudyIDs( ServerName );
        }

        public void LoadData( )
        {
            if( _SQLServerProviders.Count < 1 )
                OnStatusChanged?.Invoke( this, new IOStateChange( EIOState.Empty ) );

            if( _SQLServerProviders.Count > 0 )
            {
                UpdateServersList( );

                OnStatusChanged?.Invoke( this, new IOStateChange( EIOState.Available ) );
            }
        }

        public void RegisterServerDetailsProvider( ISQLServerProvider SQLServerProvider )
        {
            if( !_SQLServerProviders.Contains( SQLServerProvider ) )
            {
                SQLServerProvider.OnDataLoaded += _OnServerProviderDataLoaded;

                _SQLServerProviders.Add( SQLServerProvider );
            }
        }

        public void UpdateServersList( )
        {
            try
            {
                _ProcessSQLServerProviders( );

                Status = ( _ICP4SecurityServers.Count > 0 ) ? EIOState.Fallback
                                                            : EIOState.Empty;

                _InvokeDataLoaded( ELoadingState.Completed );
            }
            catch( Exception Ex )
            {
                Status = ( _ICP4SecurityServers.Count > 0 ) ? EIOState.Fallback
                                                            : EIOState.Empty;

                _InvokeDataLoaded( ( Status == EIOState.Empty ) ? ELoadingState.Failed : ELoadingState.Partial, Ex );
            }
        }
    }
}