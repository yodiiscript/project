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
        ScriptContext _scriptCon = new ScriptContext();
        List<int> _breakpoints = new List<int>();
        DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            LoadIDEConfig();
            this.ScriptCol.ItemsSource = _scriptCon.ScriptList;
            LoadYodiiSyntax();
            LoadEditorConfig();
            BreakPointsMargin.ItemsSource = _breakpoints;
            _timer.Tick += new EventHandler(SyncMarginWithLines);
            _timer.Interval = new TimeSpan( 0, 0, 0, 2 );
            _timer.Start();
            
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
                newBreakPoints.Add( i );
            }
            _breakpoints = newBreakPoints;
            BreakPointsMargin.ItemsSource = _breakpoints;
        }

        private void LoadIDEConfig()
        {
            Background = new SolidColorBrush( Colors.LightGray );
            ScriptCol.Background = new SolidColorBrush( Colors.Black );
            ScriptCol.Foreground = new SolidColorBrush( Colors.White );
            _scriptCon.Load();
            
        }

        private void button_addScript_Click( object sender, RoutedEventArgs e )
        {
            if( !String.IsNullOrEmpty( ScriptEditor.Text ) && !String.IsNullOrEmpty( entry_ScriptDesc.Text ) && !String.IsNullOrEmpty(entry_ScriptName.Text))
            {
                Script script = _scriptCon.CreateScript( entry_ScriptName.Text, "ys", entry_ScriptDesc.Text, ScriptEditor.Text );
                if(!_scriptCon.Exists(entry_ScriptName.Text))
                {
                    _scriptCon.AddScript( script );
                }
                else
                {
                    MessageBoxResult overwrite = MessageBox.Show( "A script with this name exists \n want to overwrite ?", "Script found", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No );
                    if (overwrite == MessageBoxResult.Yes ) 
                    {
                        _scriptCon.Update( script.Name, script );
                    }
                }
            }
            else
            {
                MessageBoxResult emptyField = MessageBox.Show( "At least one of the fields is empty" );
            }
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
            BreakPointsMargin.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            BreakPointsMargin.Background = new SolidColorBrush( Colors.Black );
            BreakPointsMargin.Height = ScriptEditor.Height;
        }

        private void button_deleteScript_Click( object sender, RoutedEventArgs e )
        {
            if( ScriptCol.SelectedItem != null)
            {
                int idx = ScriptCol.SelectedIndex;
                _scriptCon.Remove( _scriptCon.ScriptList[idx].Name );
            }
        }

        private void button_newScript_Click( object sender, RoutedEventArgs e )
        {
            ScriptEditor.Visibility = Visibility.Visible;
            ScriptEditor.Text = string.Empty;
        }

        private void button_editScript_Click( object sender, RoutedEventArgs e )
        {
            if( ScriptCol.SelectedItem != null )
            {
                ScriptEditor.Visibility = Visibility.Visible;
                ScriptEditor.Text = _scriptCon.ScriptList[this.ScriptCol.SelectedIndex].SourceCode;
            }
        }



    }
}
