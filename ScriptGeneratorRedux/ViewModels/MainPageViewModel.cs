using ScriptGeneratorRedux.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private FlowDocument _CurrentDocument;
        private FlowDocument _CurrentScript;
        private Boolean      _DisplayingScript;
        private Boolean      _DisplayServerInfo;
        private Boolean      _IsAddingServer;
        private INPCInvoker  _INPCInvoke;
        #endregion

        #region Relay Command Properties
        public CommandRelay<Object> AddServerCommand { get; }
        public CommandRelay<Object> CopyCommand { get; }
        public CommandRelay<Object> ExportCommand { get; }
        public CommandRelay<Object> GenerateCommand { get; }
        public CommandRelay<Object> ToggleServerInfoCommand { get; }
        public CommandRelay<Object> ValidateCommand { get; }
        public CommandRelay<Object> ViewScriptCommand { get; }
        #endregion

        public MainPageViewModel( )
        {
            _INPCInvoke             = new INPCInvoker( this );

            AddServerCommand        = new CommandRelay<Object>( AddServer );
            CopyCommand             = new CommandRelay<Object>( CopyContnet, CanCopyContnetContent );
            ExportCommand           = new CommandRelay<Object>( ExportContnet, CanExportContent );
            GenerateCommand         = new CommandRelay<Object>( GenerateScriptContnet, CanGenerateScriptContnet );
            ToggleServerInfoCommand = new CommandRelay<Object>( ToggleServerInfo );
            ValidateCommand         = new CommandRelay<Object>( ValidateScript, CanValidateScript );
            ViewScriptCommand       = new CommandRelay<Object>( ToggleDisplayingScript );
        }
        
        private void AddServer( Object Object )
        {
            if( IsAddingServer )
            {
                Core.DataContext.UpdateServersList( );
            }

            IsAddingServer = !IsAddingServer;
            _INPCInvoke.NotifyPropertyChanged( ref PropertyChanged, nameof( AddServerButtonContent ) );
        }

        private Boolean CanCopyContnetContent( object obj )
        {
            return ( Core.DataContext != null ) &&
                   !String.IsNullOrWhiteSpace( DocumentText ); ;
        }

        private Boolean CanExportContent( object obj )
        {
            return ( Core.DataContext != null ) &&
                   !String.IsNullOrWhiteSpace( DocumentText ); ;
        }

        // Will need to be updated once script interface has been formalised
        private Boolean CanGenerateScriptContnet( object obj )
        {
            return false;
        }

        // Will need to be updated once script interface has been formalised
        private Boolean CanValidateScript( object obj )
        {
            return false;
        }

        private void CopyContnet( object obj )
        {
            Core.DataContext.CopyToClipboard( CurrentDocument );
        }
        
        private void ToggleDisplayingScript( Object obj )
        {
            DisplayingScript = !DisplayingScript;
            _INPCInvoke.NotifyPropertyChanged( ref PropertyChanged, nameof( ViewScriptButtonContent ) );
        }

        private void ToggleServerInfo( object obj )
        {
            DisplayServerInfo = !DisplayServerInfo;
        }

        // Will need to be updated once script interface has been formalised
        private void ValidateScript( object obj )
        {
            // should idealy be execute appropriate core scripting method
        }

        public String AddServerButtonContent
        {
            get
            {
                return IsAddingServer ? "Confirm"
                                      : "Add Server";
            }
        }

        public FlowDocument CurrentDocument
        {
            get
            {
                return _CurrentDocument;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _CurrentDocument, value );
                _INPCInvoke.NotifyPropertyChanged( ref PropertyChanged, nameof( DisplayViewScriptButton ) );
            }
        }

        public FlowDocument CurrentScript
        {
            get
            {
                return _CurrentScript;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _CurrentScript, value );
                _INPCInvoke.NotifyPropertyChanged( ref PropertyChanged, nameof( DisplayViewScriptButton ) );
            }
        }

        public Boolean DisplayingScript
        {
            get
            {
                return _DisplayingScript;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _DisplayingScript, value );
            }
        }

        public Boolean DisplayViewScriptButton
        {
            get
            {
                return CurrentDocument != CurrentScript;
            }
        }

        public Boolean DisplayServerInfo
        {
            get
            {
                return _DisplayServerInfo;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _DisplayServerInfo, value );
            }
        }

        private String DocumentText
        {
            get
            {
                return ( CurrentDocument == null ) ? null
                                                   : ( new TextRange( CurrentDocument.ContentStart, CurrentDocument.ContentEnd ) ).Text;
            }
        }

        private void ExportContnet( object obj )
        {
            Core.DataContext.ExportToFile( CurrentDocument );
        }

        // Will need to be updated once script interface has been formalised
        private void GenerateScriptContnet( object obj )
        {
            // should idealy be execute appropriate core scripting method
        }

        public Boolean IsAddingServer
        {
            get
            {
                return _IsAddingServer;
            }
            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _IsAddingServer, value );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Will need to be updated once script interface has been formalised
        public IEnumerable<Object> ScriptList
        {
            get
            {
                return null; // should idealy be fetched from the core datacontext
            }
        }

        // Will need to be updated once script interface has been formalised
        public Object SelectedScript
        {
            get
            {
                return null; // should idealy be fetched from the core datacontext
            }

            set
            {
                // should idealy be fetched from the core datacontext
            }
        }

        public String ViewScriptButtonContent
        {
            get
            {
                return DisplayingScript ? "Hide Script"
                                        : "View Script";
            }
        }
    }
}
