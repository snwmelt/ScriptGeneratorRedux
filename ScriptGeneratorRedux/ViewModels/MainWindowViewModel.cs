using System.ComponentModel;
using System.Windows.Controls;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Variables

        private Page        _CurrentView;
        private INPCInvoker _INPCInvoke;

        #endregion

        public MainWindowViewModel( )
        {
            _INPCInvoke = new INPCInvoker( this );
        }

        public Page CurrentView
        {
            get
            {
                return _CurrentView;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue<Page>( ref PropertyChanged, ref _CurrentView, value );
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
