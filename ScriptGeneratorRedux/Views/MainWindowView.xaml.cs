using ScriptGeneratorRedux.Views.Interfaces;
using System.Windows;
using System.Windows.Navigation;

namespace ScriptGeneratorRedux.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window, INavigationServiceProvider
    {
        public MainWindowView( )
        {
            InitializeComponent( );
        }

        public NavigationService NavigationService
        {
            get
            {
                return MainWindowContentFrame.NavigationService;
            }
        }
    }
}
