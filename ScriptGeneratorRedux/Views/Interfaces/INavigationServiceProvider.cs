using System.Windows.Navigation;

namespace ScriptGeneratorRedux.Views.Interfaces
{
    internal interface INavigationServiceProvider
    {
        NavigationService NavigationService
        {
            get;
        }
    }
}
