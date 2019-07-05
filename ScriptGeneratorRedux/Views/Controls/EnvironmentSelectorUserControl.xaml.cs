using System;
using System.Collections.Generic;
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
        
        public static readonly DependencyProperty CP4EnvironmentListtProperty = DependencyProperty.Register( "CP4EnvironmentList",
                                                                                                typeof( ICollection<String> ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty CP4SecurityDatabaseListProperty = DependencyProperty.Register( "CP4SecurityDatabaseList",
                                                                                                typeof( ICollection<String> ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty CP4StudyIDListProperty = DependencyProperty.Register( "CP4StudyIDList",
                                                                                                typeof( ICollection<String> ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty ServerConnectionTestCommandProperty = DependencyProperty.Register( "ServerConnectionTestCommand",
                                                                                                typeof( CommandRelay<Object> ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );

        public static readonly DependencyProperty ServerListProperty = DependencyProperty.Register( "ServerList",
                                                                                                typeof( ICollection<String> ),
                                                                                                typeof( EnvironmentSelectorUserControl ),
                                                                                                new PropertyMetadata( null ) );
        
        #endregion

        public EnvironmentSelectorUserControl( )
        {
            InitializeComponent( );
        }

        public ICollection<String> CP4EnvironmentList
        {
            get
            {
                return ( ICollection<String> )GetValue( CP4EnvironmentListtProperty );
            }
            set
            {
                SetValue( CP4EnvironmentListtProperty, value );
            }
        }

        public ICollection<String> CP4SecurityDatabaseList
        {
            get
            {
                return ( ICollection<String> )GetValue( CP4SecurityDatabaseListProperty );
            }
            set
            {
                SetValue( CP4SecurityDatabaseListProperty, value );
            }
        }

        public ICollection<String> CP4StudyIDList
        {
            get
            {
                return ( ICollection<String> )GetValue( CP4StudyIDListProperty );
            }
            set
            {
                SetValue( CP4StudyIDListProperty, value );
            }
        }

        public ICollection<String> ServerList
        {
            get
            {
                return ( ICollection<String> )GetValue( ServerListProperty );
            }
            set
            {
                SetValue( ServerListProperty, value );
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
    }
}
