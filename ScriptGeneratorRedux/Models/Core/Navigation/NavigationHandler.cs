using System;
using System.Windows.Navigation;
using ScriptGeneratorRedux.Models.Core.Navigation.Interfaces;
using static ScriptGeneratorRedux.Models.Core.Navigation.Enums.EUIContent;

namespace ScriptGeneratorRedux.Models.Core.Navigation
{
    internal sealed class NavigationHandler : INavigationHandler
    {
        public NavigationHandler( NavigationService NavigationService )
        {
            this.NavigationService = NavigationService;
        }

        public NavigationService NavigationService
        {
            get;
        }

        public Boolean NavigationServiceAvailable
        {
            get
            {
                return !( NavigationService == null );
            }
        }

        public Boolean NavigateBackward( )
        {
            if( NavigationService.CanGoBack )
            {
                NavigationService.GoBack( );

                return true;
            }

            return false;
        }

        public Boolean NavigateForward( )
        {
            if( NavigationService.CanGoForward )
            {
                NavigationService.GoForward( );

                return true;
            }

            return false;
        }

        public Boolean NavigateTo( UIContent UI )
        {
            switch( UI )
            {
                default:
                    return NavigationService.Navigate( new Uri( "pack://application:,,,/Views/" + UI.ToString( ) + "View.xaml" ) );
            }
        }
    }
}
