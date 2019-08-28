using ScriptGeneratorRedux.Models.Core.Authentication;
using ScriptGeneratorRedux.Models.Core.Authentication.Interfaces;
using ScriptGeneratorRedux.Models.Core.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO;
using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
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

        private IDataContext                _IDataContext;
        private IDiagnosticsHandler         _IDiagnosticsHandler;
        private static readonly Core        _Instance                = new Core( );
        IUserAuthenticator                  _IUserAuthenticator;
        private Lazy<INavigationHandler>    _LazyINavigationHandler;
        private ICP4DatabaseServiceProvider _ICP4DatabaseServiceProvider;

        #endregion

        private Core( )
        {
            _IDataContext                = new DataContext( );
            _IDiagnosticsHandler         = new DiagnosticsHandler( );
            _ICP4DatabaseServiceProvider = new CP4DatabaseServiceProvider( );
            _IUserAuthenticator          = new DummyAuthManager( );
            _LazyINavigationHandler      = new Lazy<INavigationHandler>( ( ) => new NavigationHandler( ( ( INavigationServiceProvider )App.Current.MainWindow ).NavigationService ) );
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

        public static ICP4DatabaseServiceProvider CP4DatabaseService
        {
            get
            {
                return _Instance._ICP4DatabaseServiceProvider;
            }
        }

        public static IDiagnosticsHandler Diagnostics
        {
            get
            {
                return _Instance._IDiagnosticsHandler;
            }
        }
    }
}