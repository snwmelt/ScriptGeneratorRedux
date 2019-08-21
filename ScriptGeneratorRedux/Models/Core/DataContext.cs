﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using ScriptGeneratorRedux.Models.Core.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core
{
    internal class DataContext : IDataContext
    {
        #region Private Variables
        private ICollection<IDatabaseServerProvider> _ServiceDetailsProviders;
        private IDictionary<String, IDatabaseServer> _DatabaseServers;
        #endregion

        public DataContext( )
        {
            _ServiceDetailsProviders = new List<IDatabaseServerProvider>( );
            _DatabaseServers         = new Dictionary<String, IDatabaseServer>( );
        }

        public event EventHandler<LoadingEventArgs<ICollection<String>>> OnServersLoaded;
        public event EventHandler<LoadingEventArgs> OnInitialised;

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
            InvokeInitialised( ELoadingState.Completed );
        }
        
        private void InvokeInitialised( ELoadingState State, Exception Exception = null )
        {
            OnInitialised?.Invoke( this, new LoadingEventArgs( State, Exception ) );
        }
    }
}