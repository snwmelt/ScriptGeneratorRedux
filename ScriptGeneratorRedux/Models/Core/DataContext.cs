using System;
using System.Collections.Generic;
using System.Windows.Documents;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using ScriptGeneratorRedux.Models.Core.Events.Enums;
using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.Events;
using ScriptGeneratorRedux.Models.Core.IO.Database;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System.Linq;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Enums;

namespace ScriptGeneratorRedux.Models.Core
{
    internal class DataContext : IDataContext
    {
        #region Private Variables
        private ICollection<ISQLServerProvider> _SQLServerProviders;
        //private HashSet<ISQLServer>             _SecurityServers;
        private HashSet<ICP4StudyServer>        _StudyServers;
        #endregion

        public DataContext( )
        {
            _SQLServerProviders = new List<ISQLServerProvider>( );
            //_SecurityServers     = new HashSet<ISQLServer>( );
            _StudyServers        = new HashSet<ICP4StudyServer>( );
        }

        public event EventHandler<ILoadingEventArgs<IReadOnlyCollection<ISQLServer>>> OnServersLoaded;
        public event EventHandler<ILoadingEventArgs> OnInitialised;

        public void CopyToClipboard( FlowDocument currentDocument )
        {
            throw new System.NotImplementedException( );
        }

        public void ExportToFile( FlowDocument FlowDocument )
        {
            throw new System.NotImplementedException( );
        }

        public IEnumerable<String> GetEnvironmentNames( String ServerName )
        {
            return Core.CP4DatabaseService?.GetEnvironments( ( _StudyServers.Where( x => x.Name == ServerName ).FirstOrDefault( ) ) )?.Select( x => x.ToString( ) );
        }

        //TODO I Have no idea what to do here right now
        public IEnumerable<String> GetSecurityDBNames( String ServerName )
        {
            return null;
        }

        public ICollection<String> GetServerNames( )
        {
            return _StudyServers?.Select( x => x.Name )
                                 .ToList( );
        }

        public IEnumerable<Int64> GetStudyIDs( String ServerName, String EnvironmentName = null )
        {
            return Core.CP4DatabaseService?.GetStudyIDs( ServerName )?.Select( x => x.ID );
        }
        
        public void RegisterServerDetailsProvider( ISQLServerProvider SQLServerProvider )
        {
            _SQLServerProviders.Add( SQLServerProvider );
        }

        public void UpdateServersList( )
        {
            //_SecurityServers.Clear( );
            _StudyServers.Clear( );

            foreach( ISQLServerProvider SQLServerProvider in _SQLServerProviders )
            {
                if ( SQLServerProvider.Status != EIOState.Available )
                    SQLServerProvider.LoadData( );

                foreach( ISQLServer SQLServer in SQLServerProvider )
                {
                    if( SQLServer is ICP4StudyServer )
                        _StudyServers.Add( SQLServer as ICP4StudyServer );

                    //if( SQLServer is ICP4StudyServer )
                    //    _SecurityServers.Add( SQLServer );
                }
            }

            OnServersLoaded?.Invoke( this, new LoadingEventArgs<IReadOnlyCollection<ISQLServer>>( ELoadingState.Completed, _StudyServers ) );
        }

        public void Initialise( )
        {
            //var n = new IO.CP4DBO.CP4Server( "Hello World", @"011010\SQL_2012_SP4" );
            //
            //n.Initialise( );

            //_DatabaseServers.Add( n.Name, n );
            InvokeInitialised( ELoadingState.Completed );
        }
        
        private void InvokeInitialised( ELoadingState State, Exception Exception = null )
        {
            OnInitialised?.Invoke( this, new LoadingEventArgs( State, Exception ) );
        }
    }
}