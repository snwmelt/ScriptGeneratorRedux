using System.ComponentModel;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.ViewModels
{
    internal sealed class EnvironmentSelectorUserControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private INPCInvoker _INPCInvoke;
        #endregion 

        public EnvironmentSelectorUserControlViewModel( )
        {
            _INPCInvoke = new INPCInvoker( this );
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
