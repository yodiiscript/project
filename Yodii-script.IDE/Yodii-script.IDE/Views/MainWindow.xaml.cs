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

namespace Yodii_script.IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> _breakpoints = new List<int>();
        DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            LoadYodiiSyntax();
            LoadEditorConfig();
            BreakPointsMargin.ItemsSource = _breakpoints;
        }
        /// <summary>
        /// Synchronizes the number of lines and breakpoints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncMarginWithLines( object sender, EventArgs e )
        {
            List<int> newBreakPoints = new List<int>();
            int lines = ScriptEditor.LineCount;
            for( int i = 0; i < lines; i += 1 )
            {
                newBreakPoints.Add( 0 );
            }
            _breakpoints = newBreakPoints;
            BreakPointsMargin.ItemsSource = _breakpoints;
            BreakPointsMargin.ScrollIntoView( _breakpoints[_breakpoints.Count - 1] );

        }

        public void test_datagrid(Object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox t = e.EditingElement as TextBox;
            int idx = e.Row.GetIndex();
            _breakpoints[idx] = idx;
        }



            Background = new SolidColorBrush( Colors.LightGray );
            ScriptCol.Background = new SolidColorBrush( Colors.Black );
            ScriptCol.Foreground = new SolidColorBrush( Colors.White );
            _scriptCon.Load();
            if( !String.IsNullOrEmpty( ScriptEditor.Text ) && !String.IsNullOrEmpty( entry_ScriptDesc.Text ) && !String.IsNullOrEmpty(entry_ScriptName.Text))
                Script script = _scriptCon.CreateScript( entry_ScriptName.Text, "ys", entry_ScriptDesc.Text, ScriptEditor.Text );

                
            else
            {
                MessageBoxResult emptyField = MessageBox.Show( "At least one of the fields is empty" );
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
            BreakPointsMargin.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            BreakPointsMargin.Background = new SolidColorBrush( Colors.Black );
            BreakPointsMargin.Height = BreakPointsMargin.RowHeight*36;
        

                _scriptCon.CurrentScript = _scriptCon.ScriptList[this.ScriptCol.SelectedIndex];
                _scriptCon.ScriptList[this.ScriptCol.SelectedIndex].IsBeingEdited = true;
                
        }



    }
}
