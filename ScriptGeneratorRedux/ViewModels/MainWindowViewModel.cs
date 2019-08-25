using ScriptGeneratorRedux.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Variables

        private FrameworkElement _CurrentView;
        private INPCInvoker      _INPCInvoke;

        #endregion

        public MainWindowViewModel( )
        {
            _INPCInvoke = new INPCInvoker( this );
            CurrentView = new MainPageView( );
        }

        public FrameworkElement CurrentView
        {
            get
            {
                return _CurrentView;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _CurrentView, value );
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
