using ScriptGeneratorRedux.Views.Helpers.Converters.Interfaces;
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

        public static readonly DependencyProperty LexerProperty = DependencyProperty.Register( "Lexer",
                                                                                               typeof( ILexer ),
                                                                                               typeof( LexingRichTextBoxUserControl ),
                                                                                               new PropertyMetadata( null, new PropertyChangedCallback( LexerChanged ) ) );
        
        #endregion

        #region Callbacks

        private static void FlowDocumentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            LexingRichTextBoxUserControl _LexingRTBUserControl = ( d as LexingRichTextBoxUserControl );
            FlowDocument                 _FlowDocument         = ( e.NewValue as FlowDocument );
            

            ( d as RichTextBox ).Document = ( _LexingRTBUserControl.Lexer is null ) ? _FlowDocument
                                                                                    : _LexingRTBUserControl.Lexer.Parse( _FlowDocument );
        }

        private static void LexerChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ILexer _ILexer = ( e.NewValue as ILexer );

            if ( _ILexer != null )
            {
                RichTextBox _RichTextBox = ( d as RichTextBox );

                _RichTextBox.Document = _ILexer.Parse( _RichTextBox.Document );
            }
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
                return Lexer.Parse( ( FlowDocument )GetValue( FlowDocumentProperty ) );
            }

            set
            {
                SetValue( FlowDocumentProperty, value );
            }
        }

        internal ILexer Lexer
        {
            get
            {
                return ( ILexer )GetValue( LexerProperty );
            }

            set
            {
                SetValue( LexerProperty, value );
            }
        }
    }
}
