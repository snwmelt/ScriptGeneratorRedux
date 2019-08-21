using System;
using System.Collections.Generic;
using System.Windows.Documents;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using ScriptGeneratorRedux.Models.Core.Events.Enums;
using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.Events;

namespace ScriptGeneratorRedux.Models.Core
{
    internal class DataContext : IDataContext
    {
        #region Private Variables
        private ICollection<IDatabaseServerProvider> _ServiceDetailsProviders;
        //private IDictionary<String, IDatabaseServer> _DatabaseServers;
        #endregion

        public DataContext( )
        {
            int s = 0;
            _ServiceDetailsProviders = new List<IDatabaseServerProvider>( );
            //_DatabaseServers         = new Dictionary<String, IDatabaseServer>( );
        }

        public event EventHandler<ILoadingEventArgs<IReadOnlyCollection<ICP4Study>>> OnServersLoaded;
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
            return null;
        }

        public IEnumerable<String> GetSecurityDBNames( String ServerName )
        {
            return null;
        }

        public ICollection<String> GetServerNames( )
        {
            return null;
            //_DatabaseServers.Select( x => x.Key ).ToList( );
        }

        public IEnumerable<Int64> GetStudyIDs( String EnvironmentName, String SecurityDBName )
        {
            return null;
        }

        public IEnumerable<Int64> GetStudyIDs( String ServerName, String EnvironmentName, String SecurityDBName )
        {
            throw new NotImplementedException( );
        }

        public void RegisterServerDetailsProvider( IDatabaseServerProvider IServerDetailsProvider )
        {
        }

        public void UpdateServersList( )
        {
        }

        public void Initialise( )
        {
            //var n = new DatabaseServer( "Hello World", @"011010\SQL_2012_SP4" );

            //_DatabaseServers.Add( n.Name, n );
            InvokeInitialised( ELoadingState.Completed );
        }
        
        private void InvokeInitialised( ELoadingState State, Exception Exception = null )
        {
            OnInitialised?.Invoke( this, new LoadingEventArgs( State, Exception ) );
        }
    }
}