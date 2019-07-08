using ScriptGeneratorRedux.Models.Core.Authentication;
using ScriptGeneratorRedux.Models.Core.Authentication.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using ScriptGeneratorRedux.Models.Core.Navigation;
using ScriptGeneratorRedux.Models.Core.Navigation.Interfaces;
using ScriptGeneratorRedux.Views.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core
{
    internal sealed class Core
    {
        #region Private  Variables

        private IDataContext             _IDataContext;
        private static readonly Core     _Instance                = new Core( );
        IUserAuthenticator               _IUserAuthenticator;
        private Lazy<INavigationHandler> _LazyINavigationHandler;

        #endregion

        private Core( )
        {
            _IUserAuthenticator     = new DummyAuthManager( );
            _LazyINavigationHandler = new Lazy<INavigationHandler>( ( ) => new NavigationHandler( ( ( INavigationServiceProvider )App.Current.MainWindow ).NavigationService ) );
        }

        public static INavigationHandler NavigationHandler
        {
            get
            {
                return _Instance._LazyINavigationHandler.Value;
            }
        }

        public static IUserAuthenticator UserAuthenticator
        {
            get
            {
                return _Instance._IUserAuthenticator;
            }
        }

        public static IDataContext DataContext
        {
            get
            {
                return _Instance._IDataContext;
            }
        }
    }
}