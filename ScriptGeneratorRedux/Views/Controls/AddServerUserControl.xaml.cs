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

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( nameof( Password ),
                                                                                                  typeof( String ),
                                                                                                  typeof( AddServerUserControl ),
                                                                                                  new FrameworkPropertyMetadata( String.Empty,
                                                                                                                                 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        public static readonly DependencyProperty SecurityModeTextProperty = DependencyProperty.Register( nameof( SecurityModeText ),
                                                                                                          typeof( String ),
                                                                                                          typeof( AddServerUserControl ),
                                                                                                          new PropertyMetadata( "User Windows Authentication" ) );
        
        public static readonly DependencyProperty SecurityServerPlaceholderTextProperty = DependencyProperty.Register( nameof( SecurityServerPlaceholderText ),
                                                                                                                       typeof( String ),
                                                                                                                       typeof( AddServerUserControl ),
                                                                                                                       new PropertyMetadata( "Security Server Location" ) );
        
        public static readonly DependencyProperty SecurityDBNamePlaceholderTextProperty = DependencyProperty.Register( nameof( SecurityDBNamePlaceholderText ),
                                                                                                                       typeof( String ),
                                                                                                                       typeof( AddServerUserControl ),
                                                                                                                       new PropertyMetadata( "Security DB Name" ) );
        
        public static readonly DependencyProperty SecurityDBNameProperty = DependencyProperty.Register( nameof( SecurityDBName ),
                                                                                                        typeof( String ),
                                                                                                        typeof( AddServerUserControl ),
                                                                                                        new FrameworkPropertyMetadata( String.Empty,
                                                                                                                                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        public static readonly DependencyProperty SecurityModeCheckBoxTickedProperty = DependencyProperty.Register( nameof( SecurityModeCheckBoxTicked ),
                                                                                                                    typeof( Boolean ),
                                                                                                                    typeof( AddServerUserControl ),
                                                                                                                    new FrameworkPropertyMetadata( true,
                                                                                                                                                   FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                                                                                   FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) );


        public static readonly DependencyProperty SecurityServerProperty = DependencyProperty.Register( nameof( SecurityServer ),
                                                                                                        typeof( String ),
                                                                                                        typeof( AddServerUserControl ),
                                                                                                        new FrameworkPropertyMetadata( String.Empty,
                                                                                                                                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        public static readonly DependencyProperty ServerNamePlaceholderTextProperty = DependencyProperty.Register( nameof( ServerNamePlaceholderText ),
                                                                                                                   typeof( String ),
                                                                                                                   typeof( AddServerUserControl ),
                                                                                                                   new PropertyMetadata( "Security Server Name" ) );

        public static readonly DependencyProperty ServerNameProperty = DependencyProperty.Register( nameof( ServerName ),
                                                                                                    typeof( String ),
                                                                                                    typeof( AddServerUserControl ),
                                                                                                    new FrameworkPropertyMetadata( String.Empty,
                                                                                                                                   FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
        
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register( nameof( Username ),
                                                                                                  typeof( String ),
                                                                                                  typeof( AddServerUserControl ),
                                                                                                  new FrameworkPropertyMetadata( String.Empty,
                                                                                                                                 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        
        #region Accessors
        public String Password
        {
            get { return ( String )GetValue( PasswordProperty ); }
            set { SetValue( PasswordProperty, value ); }
        }

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

        public String ServerName
        {
            get { return ( String )GetValue( ServerNameProperty ); }
            set { SetValue( ServerNameProperty, value ); }
        }

        public String ServerNamePlaceholderText
        {
            get { return ( String )GetValue( ServerNamePlaceholderTextProperty ); }
            set { SetValue( ServerNamePlaceholderTextProperty, value ); }
        }

        public String Username
        {
            get { return ( String )GetValue( UsernameProperty ); }
            set { SetValue( UsernameProperty, value ); }
        }

        #endregion

        #endregion

        public AddServerUserControl( )
        {
            InitializeComponent( );
        }
    }
}
