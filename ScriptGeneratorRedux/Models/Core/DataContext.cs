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
        HashSet<ICP4StudyServer>     _ICP4StudyServer;
        HashSet<ISQLServerProvider>  _SQLServerProviders;
        #endregion

        public DataContext( )
        {
            _ICP4SecurityServers = new HashSet<ICP4SecurityServer>( );
            _ICP4StudyServer     = new HashSet<ICP4StudyServer>( );
            _SQLServerProviders  = new HashSet<ISQLServerProvider>( );
        }

        public EIOState Status => throw new NotImplementedException( );

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
            //if( String.IsNullOrWhiteSpace( ServerName ) )
            //    return _ICP4SecurityServers?.Select( x => x.SecurityDB.Where( y => y.Key.Name == "Studies" ).Select( y => y.Value ) )
            //                                .Select( x => x.);
            yield return null;
        }

        public IEnumerator<ISQLServer> GetEnumerator( )
        {
            foreach( ICP4SecurityServer ICP4SecurityServer in _ICP4SecurityServers )
                yield return ICP4SecurityServer as ISQLServer;

            foreach( ICP4StudyServer ICP4StudyServer in _ICP4StudyServer )
                yield return ICP4StudyServer as ISQLServer;
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        public IEnumerable<String> GetSecurityDBNames( String ServerName = null )
        {
            if( String.IsNullOrWhiteSpace( ServerName ) )
                return _ICP4SecurityServers.Select( x => x.SecurityDB.Name );

            return _ICP4SecurityServers.Where( x => x.Name == ServerName )
                                       ?.Select( x => x.SecurityDB.Name );
        }

        public IEnumerable<String> GetServerNames( )
        {
            return this.Select( x => x.Name );
        }

        public IEnumerable<long> GetStudyIDs( String ServerName, ECP4DepoplymentEnvironment Environment )
        {
            if( String.IsNullOrWhiteSpace( ServerName ) )
                return Core.CP4DatabaseService.GetStudyIDs( _ICP4SecurityServers, Environment );

            return Core.CP4DatabaseService.GetStudyIDs( _ICP4SecurityServers?.Where( x => x.Name == ServerName ), Environment );
        }

        public IEnumerable<long> GetStudyIDs( String ServerName = null )
        {
            if( String.IsNullOrWhiteSpace( ServerName ) )
                return Core.CP4DatabaseService.GetStudyIDs( _ICP4SecurityServers );

            return Core.CP4DatabaseService.GetStudyIDs( _ICP4SecurityServers?.Where( x => x.Name == ServerName ) );
        }

        public void LoadData( )
        {
            throw new NotImplementedException( );
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
                                              
                                              if( _ICP4StudyServer.Contains( ICP4StudyServer ) )
                                                  _ICP4StudyServer.Remove( ICP4StudyServer );

                                              _ICP4StudyServer.Add( ICP4StudyServer );
                                          }

                                          if( ISQLServer is ICP4SecurityServer )
                                          {
                                              ICP4SecurityServer ICP4SecurityServer = ISQLServer as ICP4SecurityServer;

                                              if( _ICP4SecurityServers.Contains( ICP4SecurityServer ) )
                                                  _ICP4SecurityServers.Remove( ICP4SecurityServer );

                                              ICP4SecurityServer.SecurityDB.OnDataLoaded += ( s, e ) =>
                                              {
                                                  if( e.State == ELoadingState.Completed )
                                                      _ICP4SecurityServers.Add( ICP4SecurityServer );
                                              };

                                              ICP4SecurityServer.SecurityDB?.LoadData( );
                                          }
                                      }
                                  };

                                  ISQLServerProvider.LoadData( );
                              } );

            OnDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ISQLServer>>( ELoadingState.Completed, this ) );
        }
    }
}