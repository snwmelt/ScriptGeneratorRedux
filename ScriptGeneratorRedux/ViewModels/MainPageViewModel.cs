using ScriptGeneratorRedux.Models.Core;
using System;
using System.ComponentModel;
using System.Windows.Documents;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private FlowDocument _CurrentDocument;
        private Boolean      _DisplayServerInfo;
        private INPCInvoker  _INPCInvoke;
        #endregion

        #region Relay Command Properties
        public CommandRelay<object> CopyCommand { get; }
        public CommandRelay<Object> ExportCommand { get; }
        public CommandRelay<object> ToggleServerInfoCommand { get; }
        #endregion

        public MainPageViewModel( )
        {
            _INPCInvoke             = new INPCInvoker( this );
            CopyCommand             = new CommandRelay<Object>( CopyContnet, CanCopyContnetContent );
            ExportCommand           = new CommandRelay<Object>( ExportContnet, CanExportContent );
            ToggleServerInfoCommand = new CommandRelay<Object>( ToggleServerInfo, null );
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

        private void CopyContnet( object obj )
        {
            Core.DataContext.CopyToClipboard( CurrentDocument );
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void ToggleServerInfo( object obj )
        {
            DisplayServerInfo = !DisplayServerInfo;
        }
    }
}
