﻿using ScriptGeneratorRedux.Models.Core.Authentication;
using ScriptGeneratorRedux.Models.Core.Authentication.Interfaces;
using ScriptGeneratorRedux.Models.Core.Navigation;
using ScriptGeneratorRedux.Models.Core.Navigation.Interfaces;
using ScriptGeneratorRedux.Views.Interfaces;
using System;

namespace ScriptGeneratorRedux.Models.Core
{
    internal sealed class Core
    {
        #region Private  Variables

        private static readonly Core     _Instance                = new Core( );
        IUserAuthenticator               _IUserAuthenticator;
        private Lazy<INavigationHandler> _LazyINavigationHandler;

        #endregion

        private Core( )
        {
            _IUserAuthenticator     = new DummyAuthManager( );
            _LazyINavigationHandler = new Lazy<INavigationHandler>( ( ) => new NavigationHandler( ( ( INavigationServiceProvider )App.Current.MainWindow ).NavigationService ) );
        }

        public void Dispose( )
        {
            throw new NotImplementedException( );
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
    }
}