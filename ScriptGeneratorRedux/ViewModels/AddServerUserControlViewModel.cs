using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.IO.Database;
using System;
using ScriptGeneratorRedux.Models.Core.Events.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Events.Interfaces;
using System.Collections;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Events;
using ScriptGeneratorRedux.Models.Core.Events;
using ScriptGeneratorRedux.Models.Core.Events.Enums;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO;

namespace ScriptGeneratorRedux.ViewModels
{
    internal sealed class AddServerUserControlViewModel : ISQLServerProvider
    {
        #region Private Variables
        private EIOState _Status;
        private IEnumerable<ISQLServer> _Servers;
        #endregion

        private void ClearData( )
        {                            
            Name           = String.Empty;
            TargetServer   = String.Empty;
            Password       = String.Empty;
            SecurityDBName = String.Empty;
            SecurityServer = String.Empty;
            Username       = String.Empty;

            Status = EIOState.Empty;
        }

        private void CreateServers( )
        {
            HashSet<ISQLServer> _ServersHashSet = new HashSet<ISQLServer>( );

            if( !String.IsNullOrWhiteSpace( TargetServer ) )
            {
                _ServersHashSet.Add( new CP4StudyServer( new SQLConnectionCredentials( TargetServer, UseWindowsAuthentication, Username, Password ),
                                                         Name ) );
            }

            if( !String.IsNullOrWhiteSpace( SecurityServer ) )
            {
                //TODO Add Security Server Logic and Supporting Classes
            }

            _Servers = _ServersHashSet;
        }

        public AddServerUserControlViewModel( )
        {
            UseWindowsAuthentication = true;
            Core.DataContext.RegisterServerDetailsProvider( this );
        }
        
        public event EventHandler<ILoadingEventArgs<IEnumerable<ISQLServer>>> OnDataLoaded;
        public event EventHandler<IIOStateChangedEventArgs> OnStatusChanged;

        public IEnumerator<ISQLServer> GetEnumerator( )
        {
            return _Servers.GetEnumerator( );
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
            ClearData( );

            if ( Status == EIOState.Available )
                OnDataLoaded?.Invoke( this, new LoadingEventArgs<IEnumerable<ISQLServer>>( ELoadingState.Completed, this ) );
        }
        
        public String Name
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }

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
