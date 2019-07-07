using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Walkways.MVVM.View_Model;

namespace ScriptGeneratorRedux.Views.Controls
{
    /// <summary>
    /// Interaction logic for EnvironmentSelectorUserControl.xaml
    /// </summary>
    public partial class EnvironmentSelectorUserControl : UserControl
    {
        #region Dependency Properties
        
        public static readonly DependencyProperty CP4EnvironmentListProperty = DependencyProperty.Register( "CP4EnvironmentList",
                                                                                                typeof( IEnumerable ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedCP4EnvironmentProperty = DependencyProperty.Register( "SelectedCP4Environment",
                                                                                                typeof( Object ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedCP4EnvironmentIndexProperty = DependencyProperty.Register( "SelectedCP4EnvironmentIndex",
                                                                                                typeof( int ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( -1 ) );


        public static readonly DependencyProperty CP4SecurityDatabaseListProperty = DependencyProperty.Register( "CP4SecurityDatabaseList",
                                                                                                typeof( IEnumerable ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedCP4SecurityDatabaseProperty = DependencyProperty.Register( "SelectedCP4SecurityDatabase",
                                                                                                typeof( Object ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedCP4SecurityDatabaseIndexProperty = DependencyProperty.Register( "SelectedCP4SecurityDatabaseIndex",
                                                                                                typeof( int ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( -1 ) );


        public static readonly DependencyProperty CP4StudyIDListProperty = DependencyProperty.Register( "CP4StudyIDList",
                                                                                                typeof( IEnumerable ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedCP4StudyIDProperty = DependencyProperty.Register( "SelectedCP4StudyID",
                                                                                                typeof( Object ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedCP4StudyIDIndexProperty = DependencyProperty.Register( "SelectedCP4StudyIDIndex",
                                                                                                typeof( int ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( -1 ) );


        public static readonly DependencyProperty ServerConnectionTestCommandProperty = DependencyProperty.Register( "ServerConnectionTestCommand",
                                                                                                typeof( CommandRelay<Object> ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );


        public static readonly DependencyProperty ServerListProperty = DependencyProperty.Register( "ServerList",
                                                                                                typeof( IEnumerable ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedServerProperty = DependencyProperty.Register( "SelectedServer",
                                                                                                typeof( Object ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty SelectedServerIndexProperty = DependencyProperty.Register( "SelectedServerIndex",
                                                                                                typeof( int ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( -1 ) );

        #endregion

        public EnvironmentSelectorUserControl( )
        {
            InitializeComponent( );
        }

        public IEnumerable CP4EnvironmentList
        {
            get
            {
                return ( IEnumerable )GetValue( CP4EnvironmentListProperty );
            }

            set
            {
                SetValue( CP4EnvironmentListProperty, value );
            }
        }

        public Object SelectedCP4Environment
        {
            get
            {
                return ( Object )GetValue( SelectedCP4EnvironmentProperty );
            }

            set
            {
                SetValue( SelectedCP4EnvironmentProperty, value );
            }
        }

        public int SelectedCP4EnvironmentIndex
        {
            get
            {
                return ( int )GetValue( SelectedCP4EnvironmentIndexProperty );
            }

            set
            {
                SetValue( SelectedCP4EnvironmentIndexProperty, value );
            }
        }


        public IEnumerable CP4SecurityDatabaseList
        {
            get
            {
                return ( IEnumerable )GetValue( CP4SecurityDatabaseListProperty );
            }
            set
            {
                SetValue( CP4SecurityDatabaseListProperty, value );
            }
        }

        public Object SelectedCP4SecurityDatabase
        {
            get
            {
                return ( Object )GetValue( SelectedCP4SecurityDatabaseProperty );
            }
            set
            {
                SetValue( SelectedCP4SecurityDatabaseProperty, value );
            }
        }

        public int SelectedCP4SecurityDatabaseIndex
        {
            get
            {
                return ( int )GetValue( SelectedCP4SecurityDatabaseIndexProperty );
            }
            set
            {
                SetValue( SelectedCP4SecurityDatabaseIndexProperty, value );
            }
        }

        public IEnumerable CP4StudyIDList
        {
            get
            {
                return ( IEnumerable )GetValue( CP4StudyIDListProperty );
            }
            set
            {
                SetValue( CP4StudyIDListProperty, value );
            }
        }

        public Object SelectedCP4StudyID
        {
            get
            {
                return ( Object )GetValue( SelectedCP4StudyIDProperty );
            }
            set
            {
                SetValue( SelectedCP4StudyIDProperty, value );
            }
        }

        public int SelectedCP4StudyIDIndex
        {
            get
            {
                return ( int )GetValue( SelectedCP4StudyIDIndexProperty );
            }
            set
            {
                SetValue( SelectedCP4StudyIDIndexProperty, value );
            }
        }

        public CommandRelay<Object> ServerConnectionTestCommand
        {
            get
            {
                return ( CommandRelay<Object> )GetValue( ServerConnectionTestCommandProperty );
            }
            set
            {
                SetValue( ServerConnectionTestCommandProperty, value );
            }
        }

        public IEnumerable ServerList
        {
            get
            {
                return ( IEnumerable )GetValue( ServerListProperty );
            }
            set
            {
                SetValue( ServerListProperty, value );
            }
        }

        public Object SelectedServer
        {
            get
            {
                return ( Object )GetValue( SelectedServerProperty );
            }
            set
            {
                SetValue( SelectedServerProperty, value );
            }
        }

        public int SelectedServerIndex
        {
            get
            {
                return ( int )GetValue( SelectedServerIndexProperty );
            }
            set
            {
                SetValue( SelectedServerIndexProperty, value );
            }
        }
    }
}
