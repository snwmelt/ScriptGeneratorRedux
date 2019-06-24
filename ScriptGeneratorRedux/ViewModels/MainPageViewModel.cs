using System.ComponentModel;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private INPCInvoker _INPCInvoke;

        public MainPageViewModel( )
        {
            _INPCInvoke = new INPCInvoker( this );
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
