using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal sealed class EnvironmentSelectorUserControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private IEnumerable<String> _CP4Environments;
        private IEnumerable<String> _CP4SecurityDatabases;
        private IEnumerable<Int64>  _CP4StudyIDs;
        private INPCInvoker         _INPCInvoker;
        private String              _SelectedCP4Environment;
        private String              _SelectedCP4SecurityDatabase;
        private String              _SelectedServer;
        private ICollection<String> _Servers;
        #endregion

        public EnvironmentSelectorUserControlViewModel( )
        {
            _INPCInvoker = new INPCInvoker( this );
            Servers      = Core.DataContext.GetServerNames( );

            Core.DataContext.OnServersLoaded += DataContext_OnServersLoaded;
        }

        private void DataContext_OnServersLoaded( Object sender, ILoadingEventArgs<IReadOnlyCollection<ICP4Study>> e )
        {
            // parellel work to load names

            Servers.Clear( );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( HasData ) );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Boolean HasData
        {
            get
            {
                return ( Servers?.Count > 0 );
            }
        }

        public IEnumerable<String> CP4Environments
        {
            get
            {
                return _CP4Environments;
            }
            private set
            {
                _INPCInvoker.AssignPropertyValue( ref PropertyChanged, ref _CP4Environments, value );
            }
        }

        public IEnumerable<long> CP4StudyIDs
        {
            get
            {
                return _CP4StudyIDs;
            }
            private set
            {
                _INPCInvoker.AssignPropertyValue( ref PropertyChanged, ref _CP4StudyIDs, value );
            }
        }

        public IEnumerable<String> CP4SecurityDatabases
        {
            get
            {
                return _CP4SecurityDatabases;
            }
            private set
            {
                _INPCInvoker.AssignPropertyValue( ref PropertyChanged, ref _CP4SecurityDatabases, value );
            }
        }

        public string SelectedCP4Environment
        {
            get
            {
                return _SelectedCP4Environment;
            }

            set
            {
                CP4StudyIDs             = Core.DataContext.GetStudyIDs( SelectedServer, value, SelectedCP4SecurityDatabase );
                _SelectedCP4Environment = value;
            }
        }

        public string SelectedCP4SecurityDatabase
        {
            get
            {
                return _SelectedCP4SecurityDatabase;
            }

            set
            {
                CP4StudyIDs                  = Core.DataContext.GetStudyIDs( SelectedServer, SelectedCP4Environment, value  );
                _SelectedCP4SecurityDatabase = value;
            }
        }

        public string SelectedCP4StudyID
        {
            get;
            set;
        }

        public String SelectedServer
        {
            get
            {
                return _SelectedServer;
            }

            set
            {
                CP4Environments      = Core.DataContext.GetEnvironmentNames( value );
                CP4SecurityDatabases = Core.DataContext.GetSecurityDBNames( value );
                _SelectedServer      = value;
            }
        }

        public ICollection<String> Servers
        {
            get
            {
                return _Servers;
            }
            private set
            {
                _INPCInvoker.AssignPropertyValue( ref PropertyChanged, ref _Servers, value );
            }
        }
    }
}
