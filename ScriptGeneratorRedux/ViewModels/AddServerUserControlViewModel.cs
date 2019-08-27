using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.IO.Database;
using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System.Collections;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO;
using System.ComponentModel;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal sealed class AddServerUserControlViewModel : ISQLServerProvider, INotifyPropertyChanged
    {
        #region Private Variables
        private EIOState _Status;
        private IEnumerable<ISQLServer> _Servers;
        INPCInvoker _INPCInvoker;
        #endregion

        private void ClearData( )
        {                            
            Name           = String.Empty;
            TargetServer   = String.Empty;
            Password       = String.Empty;
            SecurityDBName = String.Empty;
            SecurityServer = String.Empty;
            Username       = String.Empty;

            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( Name ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( TargetServer ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( Password ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( SecurityDBName ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( SecurityServer ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( Username ) );

            Status = EIOState.Empty;
        }

        private void CreateServers( )
        {
            HashSet<ISQLServer> _ServersHashSet = new HashSet<ISQLServer>( );

            if( !String.IsNullOrWhiteSpace( TargetServer ) )
            {
                _ServersHashSet.Add( new CP4StudyServer( UseWindowsAuthentication ? new SQLConnectionCredentials( TargetServer )
                                                                                  : new SQLConnectionCredentials( TargetServer, Username, Password ),
                                                         Name ) );
            }

            if( !String.IsNullOrWhiteSpace( SecurityServer ) )
            {
                _ServersHashSet.Add( new CP4SecurityServer( UseWindowsAuthentication ? new SQLConnectionCredentials( TargetServer )
                                                                                     : new SQLConnectionCredentials( TargetServer, Username, Password ),
                                                            Name,
                                                            SecurityDBName ) );
            }

            _Servers = _ServersHashSet;
        }

        public AddServerUserControlViewModel( )
        {
            UseWindowsAuthentication = true;
            Core.DataContext.RegisterServerDetailsProvider( this );
            _INPCInvoker = new INPCInvoker( this );
        }
        
        public IEnumerator<ISQLServer> GetEnumerator( )
        {
            return _Servers?.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        public void LoadData( )
        {
            if( String.IsNullOrWhiteSpace( TargetServer ) &&
                String.IsNullOrWhiteSpace( SecurityServer ) )
            {
                Status = EIOState.Invalid;
            }
            else if( !String.IsNullOrWhiteSpace( TargetServer ) ||
                     !String.IsNullOrWhiteSpace( SecurityServer ) )
            {
                if( !UseWindowsAuthentication &&
                    String.IsNullOrWhiteSpace( Username ) )
                {
                    Status = EIOState.Invalid;
                }
                else
                {
                    Status = EIOState.Available;
                }
            }

            CreateServers( );
            
            if ( Status == EIOState.Available )
                OnDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ISQLServer>>( ELoadingState.Completed, this ) );

            ClearData( );
        }

        public String Name
        {
            get;
            set;
        }
        
        public event EventHandler<ILoadingEventArgs<IEnumerable<ISQLServer>>> OnDataLoaded;
        public event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;
        
        public String Password
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public String SecurityDBName
        {
            get;
            set;
        }

        public String SecurityServer
        {
            get;
            set;
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
                    OnStatusChanged?.Invoke( this, new IOStateChange( Status ) );
                }
            }
        }

        public String TargetServer
        {
            get;
            set;
        }

        public String Username
        {
            get;
            set;
        }

        public Boolean UseWindowsAuthentication
        {
            get;
            set;
        }
    }
}
