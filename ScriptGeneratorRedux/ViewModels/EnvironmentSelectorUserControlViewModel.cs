using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal sealed class EnvironmentSelectorUserControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private INPCInvoker _INPCInvoker;
        private String      _SelectedCP4Environment;
        private String      _SelectedCP4SecurityDatabase;
        private String      _SelectedServer;
        #endregion

        public EnvironmentSelectorUserControlViewModel( )
        {
            _INPCInvoker = new INPCInvoker( this );

            Core.DataContext.OnServersLoaded += DataContext_OnServersLoaded;
        }

        private void DataContext_OnServersLoaded( Object sender, ILoadingEventArgs<IReadOnlyCollection<ISQLServer>> e )
        {
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( HasData ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( Servers ) );
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
                return Core.DataContext.GetEnvironmentNames( SelectedServer );
            }
        }

        public IEnumerable<long> CP4StudyIDs
        {
            get
            {
                return Core.DataContext.GetStudyIDs( SelectedServer, SelectedCP4Environment );
            }
        }

        // TODO Implement data flow path for selecting what group of security servers will be assosiated with a selection
        public IEnumerable<String> CP4SecurityDatabases
        {
            get
            {
                return Core.DataContext.GetSecurityDBNames( SelectedServer );
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
                if( _SelectedCP4Environment != value )
                {
                    _SelectedCP4Environment = value;
                    _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( CP4StudyIDs ) );
                }
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
                if ( _SelectedCP4SecurityDatabase != value )
                {
                    _SelectedCP4SecurityDatabase = value;
                    _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( CP4StudyIDs ) );
                }
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
                if( _SelectedServer != value )
                {
                    _SelectedServer = value;
                    _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( CP4Environments ) );
                    _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( CP4StudyIDs ) );
                }
            }
        }
        public ICollection<String> Servers
        {
            get
            {
                return Core.DataContext.GetServerNames( );
            }
        }
    }
}
