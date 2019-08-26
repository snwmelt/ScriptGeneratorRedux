using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System.Linq;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;

namespace ScriptGeneratorRedux.Models.Core
{
    internal class DataContext : IDataContext
    {
        #region Private Variables
        HashSet<ICP4SecurityServer>  _ICP4SecurityServers;
        Object                       _ICP4SecurityServersLock;
        HashSet<ICP4StudyServer>     _ICP4StudyServers;
        Object                       _ICP4StudyServersLock;
        HashSet<ISQLServerProvider>  _SQLServerProviders;
        #endregion

        public DataContext( )
        {
            _ICP4SecurityServers     = new HashSet<ICP4SecurityServer>( );
            _ICP4SecurityServersLock = new Object( );
            _ICP4StudyServers        = new HashSet<ICP4StudyServer>( );
            _ICP4StudyServersLock    = new Object( );
            _SQLServerProviders      = new HashSet<ISQLServerProvider>( );
        }

        public EIOState Status
        {
            get
            {
                throw new NotImplementedException( );
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
            if( String.IsNullOrEmpty( ServerName ) )
                return Core.CP4DatabaseService?.GetEnvironments( this )?.Select( x => x.ToString( ) );

            return Core.CP4DatabaseService?.GetEnvironments( this.Where( x => x.Name == ServerName ) )?.Select( x => x.ToString( ) ); ;
        }

        public IEnumerator<ISQLServer> GetEnumerator( )
        {
            foreach( ICP4SecurityServer ICP4SecurityServer in _ICP4SecurityServers )
                yield return ICP4SecurityServer as ISQLServer;

            foreach( ICP4StudyServer ICP4StudyServer in _ICP4StudyServers )
                yield return ICP4StudyServer as ISQLServer;
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        public IEnumerable<String> GetSecurityDBNames( String ServerName = null )
        {
            if( String.IsNullOrWhiteSpace( ServerName ) )
                return _ICP4SecurityServers?.Select( x => x.SecurityDB.Name );

            return _ICP4SecurityServers?.Where( x => x.Name == ServerName )
                                       ?.Select( x => x.SecurityDB.Name );
        }

        public IEnumerable<String> GetServerNames( )
        {
            return this.Select( x => x.Name );
        }

        public IEnumerable<long> GetStudyIDs( String ServerName, ECP4DepoplymentEnvironment Environment )
        {
            if( String.IsNullOrWhiteSpace( ServerName ) )
                return Core.CP4DatabaseService?.GetStudyIDs( this, Environment );

            return Core.CP4DatabaseService?.GetStudyIDs( this?.Where( x => x.Name == ServerName ), Environment );
        }

        public IEnumerable<long> GetStudyIDs( String ServerName = null )
        {
            if( String.IsNullOrWhiteSpace( ServerName ) )
                return Core.CP4DatabaseService?.GetStudyIDs( this );

            return Core.CP4DatabaseService?.GetStudyIDs( this?.Where( x => x.Name == ServerName ) );
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
            _SQLServerProviders.Add( SQLServerProvider );
        }

        public void UpdateServersList( )
        {
            Parallel.ForEach( _SQLServerProviders, 
                              ( ISQLServerProvider ) =>
                              {
                                  ISQLServerProvider.OnDataLoaded += ( se, ev ) =>
                                  {
                                      foreach( ISQLServer ISQLServer in ev.Payload )
                                      {
                                          if( ISQLServer is ICP4StudyServer )
                                          {
                                              ICP4StudyServer ICP4StudyServer = ISQLServer as ICP4StudyServer;

                                              lock ( _ICP4StudyServersLock )
                                              {
                                                  if ( _ICP4StudyServers.Contains( ICP4StudyServer ) )
                                                      _ICP4StudyServers.Remove( ICP4StudyServer );

                                                  _ICP4StudyServers.Add( ICP4StudyServer );
                                              }
                                          }

                                          if( ISQLServer is ICP4SecurityServer )
                                          {
                                              ICP4SecurityServer ICP4SecurityServer = ISQLServer as ICP4SecurityServer;

                                              ICP4SecurityServer.SecurityDB?.LoadData( );

                                              lock ( _ICP4SecurityServersLock )
                                              {
                                                  if ( _ICP4SecurityServers.Contains( ICP4SecurityServer ) )
                                                      _ICP4SecurityServers.Remove( ICP4SecurityServer );

                                                  _ICP4SecurityServers.Add( ICP4SecurityServer );

                                              }
                                          }
                                      }
                                  };

                                  ISQLServerProvider.LoadData( );
                              } );

            OnDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ISQLServer>>( ELoadingState.Completed, this ) );
        }
    }
}