using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ScriptGeneratorRedux.Views.Controls
{
    /// <summary>
    /// Interaction logic for LexingRichTextBoxUserControl.xaml
    /// </summary>
    public partial class LexingRichTextBoxUserControl : RichTextBox
    {
        #region Dependency Properties
        public static readonly DependencyProperty FlowDocumentProperty = DependencyProperty.Register( "FlowDocument",
                                                                                                typeof( FlowDocument ),
                                                                                                typeof( LexingRichTextBoxUserControl ),
                                                                                                new PropertyMetadata( null, new PropertyChangedCallback( FlowDocumentChanged ) ) );

        #endregion

        #region Callbacks

        private static void FlowDocumentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as RichTextBox ).Document = ( e.NewValue as FlowDocument );
        }

        #endregion

        public LexingRichTextBoxUserControl( )
        {
            InitializeComponent( );
        }

        public FlowDocument FlowDocument
        {
            get
            {
                return ( FlowDocument )GetValue( FlowDocumentProperty );
            }

            set
            {
                SetValue( FlowDocumentProperty, value );
            }
        }
    }
}
