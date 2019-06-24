using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.Extensions;
using System;
using System.ComponentModel;
using Walkways.MVVM.View_Model;
using static ScriptGeneratorRedux.Models.Core.Navigation.Enums.EUIContent;

namespace ScriptGeneratorRedux.ViewModels
{
    internal class LoginPageViewModel : INotifyPropertyChanged
    {
        #region Private Variables

        private INPCInvoker _INPCInvoke;
        private Char        _MaskChar       = '*';
        private String      _Password       = String.Empty;
        private String      _Username       = String.Empty;
        private Boolean     _InvalidAttempt = false;

        #endregion

        public LoginPageViewModel( )
        {
            _INPCInvoke         = new INPCInvoker( this );
            ValidateUserCommand = new CommandRelay<Object>( ValidateUser, CanValidateUser );
        }

        private bool CanValidateUser( Object obj )
        {
            return Core.UserAuthenticator.IsAValidPassword( _Password ) &&
                   Core.UserAuthenticator.IsAValidUsername( Username );
        }

        public Boolean InvalidAttempt
        {
            private get
            {
                return _InvalidAttempt;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _InvalidAttempt, value );
            }
        }

        public String Password
        {
            private get
            {
                return new String( _MaskChar, _Password.Length );
            }

            set
            {
                _Password = _Password.EntangledUpdate( value );

                _INPCInvoke.NotifyPropertyChanged( ref PropertyChanged );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public String Username
        {
            private get
            {
                return _Username;
            }

            set
            {
                _INPCInvoke.AssignPropertyValue( ref PropertyChanged, ref _Username, value );
            }
        }

        public void ValidateUser( Object obj )
        {
            Core.UserAuthenticator.Authenticate( Username, _Password );

            if( Core.UserAuthenticator.HasAuthenticatedUser )
            {
                Core.NavigationHandler.NavigateTo( UIContent.MainPage );
            }
        }

        public CommandRelay<object> ValidateUserCommand
        {
            get;
        }
    }
}
