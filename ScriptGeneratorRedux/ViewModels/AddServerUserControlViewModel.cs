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
    internal sealed class AddServerUserControlViewModel : AIEnumerableDataSource<ISQLServer>, ISQLServerProvider, INotifyPropertyChanged
    {
        #region Private Variables
        INPCInvoker       _INPCInvoker;
        CP4SecurityServer _Server;
        #endregion

        private void ClearData( )
        {                            
            ServerName     = String.Empty;
            Password       = String.Empty;
            SecurityDBName = String.Empty;
            SecurityServer = String.Empty;
            Username       = String.Empty;

            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( ServerName ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( Password ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( SecurityDBName ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( SecurityServer ) );
            _INPCInvoker.NotifyPropertyChanged( ref PropertyChanged, nameof( Username ) );

            Status = EIOState.Empty;
        }

        private void CreateServer( )
        {
            _Server = new CP4SecurityServer( UseWindowsAuthentication ? new SQLConnectionCredentials( $"Server={SecurityServer}" )
                                                                      : new SQLConnectionCredentials( $"Server={SecurityServer}", Username, Password ),
                                             ServerName,
                                             SecurityDBName );
        }

        public AddServerUserControlViewModel( )
        {
            UseWindowsAuthentication = true;
            Core.DataContext.RegisterServerDetailsProvider( this );
            _INPCInvoker = new INPCInvoker( this );
        }
        
        public override IEnumerator<ISQLServer> GetEnumerator( )
        {
            yield return _Server;
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        public override void LoadData( )
        {
            if( String.IsNullOrWhiteSpace( SecurityServer ) )
            {
                Status = EIOState.Invalid;
            }
            else if( !String.IsNullOrWhiteSpace( SecurityServer ) )
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

            if ( Status == EIOState.Available )
            {
                try
                {
                    CreateServer( );

                    InvokeDataLoaded( ELoadingState.Completed );
                }
                catch ( Exception Ex )
                {
                    InvokeDataLoaded( ELoadingState.Failed, Ex );
                }
            }
            else
            {
                InvokeDataLoaded( ELoadingState.Failed, new InvalidOperationException( "AddServerUserControlViewModel" ) );
            }

            ClearData( );
        }

        public String ServerName
        {
            get;
            set;
        }
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
