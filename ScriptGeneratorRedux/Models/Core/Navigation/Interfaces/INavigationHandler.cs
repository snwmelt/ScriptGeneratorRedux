using System;
using System.Windows.Navigation;
using static ScriptGeneratorRedux.Models.Core.Navigation.Enums.EUIContent;

namespace ScriptGeneratorRedux.Models.Core.Navigation.Interfaces
{
    internal interface INavigationHandler
    {
        /// <summary>
        /// Naviage user interface Backwards.
        /// </summary>
        /// <returns></returns>
        Boolean NavigateBackward( );

        /// <summary>
        /// Navigate user interface Forwards.
        /// </summary>
        /// <returns></returns>
        Boolean NavigateForward( );

        /// <summary>
        /// An Instance of System.Windows.Navigation.NavigationService.
        /// </summary>
        NavigationService NavigationService
        {
            get;
        }

        /// <summary>
        /// Indicate availablity of the System.Windows.Navigation.NavigationService Instance.
        /// </summary>
        Boolean NavigationServiceAvailable
        {
            get;
        }

        /// <summary>
        /// Navigate user interface to a Page assosiated with the UIContent Enum. 
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        Boolean NavigateTo( UIContent UI );
    }
}
