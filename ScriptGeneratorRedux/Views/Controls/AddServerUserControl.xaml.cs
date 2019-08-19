using System;
using System.Windows;
using System.Windows.Controls;

namespace ScriptGeneratorRedux.Views.Controls
{
    /// <summary>
    /// Interaction logic for AddServerUserControl.xaml
    /// </summary>
    public partial class AddServerUserControl : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty SecurityModeTextProperty = DependencyProperty.Register( nameof( SecurityModeText ),
                                                                                                          typeof( String ),
                                                                                                          typeof( AddServerUserControl ),
                                                                                                          new PropertyMetadata( "User Windows Authentication" ) );

        public static readonly DependencyProperty TargetServerPlaceholderTextProperty = DependencyProperty.Register( nameof( TargetServerPlaceholderText ),
                                                                                                                     typeof( String ),
                                                                                                                     typeof( AddServerUserControl ),
                                                                                                                     new PropertyMetadata( "CP4 Server" ) );
        
        public static readonly DependencyProperty SecurityServerPlaceholderTextProperty = DependencyProperty.Register( nameof( SecurityServerPlaceholderText ),
                                                                                                                       typeof( String ),
                                                                                                                       typeof( AddServerUserControl ),
                                                                                                                       new PropertyMetadata( "Security Server" ) );
        
        public static readonly DependencyProperty SecurityDBNamePlaceholderTextProperty = DependencyProperty.Register( nameof( SecurityDBNamePlaceholderText ),
                                                                                                                       typeof( String ),
                                                                                                                       typeof( AddServerUserControl ),
                                                                                                                       new PropertyMetadata( "Security DB Name" ) );
        
        public static readonly DependencyProperty SecurityDBNameProperty = DependencyProperty.Register( nameof( SecurityDBName ),
                                                                                                        typeof( String ),
                                                                                                        typeof( AddServerUserControl ),
                                                                                                        new PropertyMetadata( String.Empty ) );

        public static readonly DependencyProperty SecurityModeCheckBoxTickedProperty = DependencyProperty.Register( nameof( SecurityModeCheckBoxTicked ),
                                                                                                                    typeof( Boolean ),
                                                                                                                    typeof( AddServerUserControl ),
                                                                                                                    new PropertyMetadata( true ) );


        public static readonly DependencyProperty SecurityServerProperty = DependencyProperty.Register( nameof( SecurityServer ),
                                                                                                        typeof( String ),
                                                                                                        typeof( AddServerUserControl ),
                                                                                                        new PropertyMetadata( String.Empty, SecurityServerChangedCallback ) );
        
        public static readonly DependencyProperty TargetServerProperty = DependencyProperty.Register( nameof( TargetServer ),
                                                                                                      typeof( String ),
                                                                                                      typeof( AddServerUserControl ),
                                                                                                      new PropertyMetadata( String.Empty, TargetServerChangedCallback ) );

        #region Callbacks

        private static void SecurityServerChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AddServerUserControl Instance       = ( d as AddServerUserControl );
            String               SecurityServer = ( e.NewValue as String );

            if( String.IsNullOrWhiteSpace( SecurityServer ) )
            {
                Instance.SecurityServerIsCustom = false;
                Instance.SecurityServer         = Instance.TargetServer;
            }
            else 
            {
                if( SecurityServer != Instance.TargetServer )
                {
                    Instance.SecurityServerIsCustom = true;
                }
            }
        }

        private static void TargetServerChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AddServerUserControl Instance = ( d as AddServerUserControl );

            if( !Instance.SecurityServerIsCustom )
            {
                Instance.SecurityServer = ( e.NewValue as String );
            }
        }

        #endregion

        #region Accessors

        public String SecurityDBName
        {
            get { return ( String )GetValue( SecurityDBNameProperty ); }
            set { SetValue( SecurityDBNameProperty, value ); }
        }

        public String SecurityDBNamePlaceholderText
        {
            get { return ( String )GetValue( SecurityDBNamePlaceholderTextProperty ); }
            set { SetValue( SecurityDBNamePlaceholderTextProperty, value ); }
        }
        
        public String SecurityModeText
        {
            get { return ( String )GetValue( SecurityModeTextProperty ); }
            set { SetValue( SecurityModeTextProperty, value ); }
        }

        public Boolean SecurityModeCheckBoxTicked
        {
            get { return ( Boolean )GetValue( SecurityModeCheckBoxTickedProperty ); }
            set { SetValue( SecurityModeCheckBoxTickedProperty, value ); }
        }

        public String SecurityServer
        {
            get { return ( String )GetValue( SecurityServerProperty ); }
            set { SetValue( SecurityServerProperty, value ); }
        }

        public String SecurityServerPlaceholderText
        {
            get { return ( String )GetValue( SecurityServerPlaceholderTextProperty ); }
            set { SetValue( SecurityServerPlaceholderTextProperty, value ); }
        }

        public String TargetServerPlaceholderText
        {
            get { return ( String )GetValue( TargetServerPlaceholderTextProperty ); }
            set { SetValue( TargetServerPlaceholderTextProperty, value ); }
        }

        public String TargetServer
        {
            get { return ( String )GetValue( TargetServerProperty ); }
            set { SetValue( TargetServerProperty, value ); }
        }

        #endregion

        #endregion

        public AddServerUserControl( )
        {
            InitializeComponent( );
        }

        public Boolean SecurityServerIsCustom
        {
            get;
            private set;
        }
    }
}
