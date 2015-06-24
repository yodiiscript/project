using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Editing;
using Yodii_script.IDE.View_Models;
using System.Xml;
using GUI;
using Yodii.Script;
using Yodii.Script.Debugger;

namespace Yodii_script.IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScriptContext _scriptCon = new ScriptContext();
        List<bool?> _breakpoints = new List<bool?>();
        Watch _watches;


        public MainWindow()
        {
            InitializeComponent();
            LoadYodiiSyntax();
            LoadEditorConfig();
                        
        }
        private void LoadYodiiSyntax()
        {
            // Load a different syntax config file
            System.IO.StreamReader s = new System.IO.StreamReader( @"../../../ys.xshd" );
            {
                using( XmlTextReader reader = new XmlTextReader( s ) )
                {
                    this.ScriptEditor.SyntaxHighlighting = HighlightingLoader.Load( reader, HighlightingManager.Instance );
                }
            }
        }
        private void LoadEditorConfig()
        {
            this.ScriptEditor.ShowLineNumbers = true;
            this.ScriptEditor.LineNumbersForeground = new SolidColorBrush( Colors.Yellow );
            this.ScriptEditor.WordWrap = true;
            this.ScriptEditor.Background = new SolidColorBrush( Colors.Black );
            this.ScriptEditor.Foreground = new SolidColorBrush( Colors.White );
            BreakPointsMargin.Background = new SolidColorBrush( Colors.White );
            BreakPointsMargin.Height = ScriptEditor.Height;
        }

        /// <summary>
        /// Synchronizes the number of lines and breakpoints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncMarginWithLines( object sender, EventArgs e )
        {
            BreakPointsMargin.Items.Clear();
            if (!String.IsNullOrEmpty(ScriptEditor.Text) )
            {
                Expr exp = ExprAnalyser.AnalyseString( ScriptEditor.Text );
                BreakableVisitor bkv = new BreakableVisitor();
                bkv.VisitExpr( exp );
                foreach( var item in bkv.BreakableExprs )
                {
                    CheckBox cb = new CheckBox();
                    cb.IsChecked = false;

                    cb.Margin = new Thickness( 1 );
                    
                    if( item == null )
                    {
                        _breakpoints.Add( null );
                        cb.IsEnabled = false;

                    }
                    else
                    {
                        _breakpoints.Add( false );
                        cb.IsEnabled = true;
                    }
                    BreakPointsMargin.Items.Add( cb );
                }
            }
        }

        private void Debug_Click( object sender, RoutedEventArgs e )
        {
            var a = sv1.ScrollableHeight;
            var b = sv2.ScrollableHeight;
            if( StackTest.Children.Count == 0 )
            {
                _watches = new Watch( this );
                StackTest.Children.Add( _watches );
            }
            else
            {
                MessageBoxResult popup = MessageBox.Show("Debug already running");
            }
        }
        private void ScrollChanged( object sender, ScrollChangedEventArgs e )
        {
            if( sender == sv1 )
            {
                sv2.ScrollToVerticalOffset( e.VerticalOffset );
            }
            else
            {
                sv1.ScrollToVerticalOffset( e.VerticalOffset );
            }
        }
    }
}
