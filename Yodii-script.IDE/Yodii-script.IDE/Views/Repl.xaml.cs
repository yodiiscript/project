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
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Editing;
using System.Xml;
using Yodii.Script.Debugger;
using Yodii.Script;

namespace Yodii_script.IDE.Views
{
    /// <summary>
    /// Interaction logic for Repl.xaml
    /// </summary>
    public partial class Repl : UserControl
    {
        ScriptEngineDebugger _engine = new ScriptEngineDebugger( new GlobalContext() );
        ScriptEngine.Result _res;
        MainWindow _root;

        public Repl( MainWindow root )
        {
            this.Height = 500;
            _root = root;
            InitializeComponent();
            ConsoleInput.LineNumbersForeground = new SolidColorBrush( Colors.Yellow );
            ConsoleInput.WordWrap = true;
            ConsoleOutput.MinHeight = 110;
            ConsoleOutput.MaxHeight = 160;
            ConsoleOutput.SelectionMode = SelectionMode.Single;
            ConsoleOutput.Focusable = false;
            //ConsoleOutput.IsEnabled = false;
            ConsoleOutput.Background = new SolidColorBrush( Colors.Black );
            LoadYodiiSyntax();
        }
        /// <summary>
        /// Retrieving code and executing it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteConsole_Click(object sender, RoutedEventArgs e)
        {
            string codeToEx;
            if( ConsoleInput.SelectedText == "" )
            {
                codeToEx = ConsoleInput.Text;
            }
            else
            {
                codeToEx = ConsoleInput.SelectedText;
            }
            Expr exp = ExprAnalyser.AnalyseString( codeToEx );
            _res = _engine.Execute( exp );
            ConsoleOutput.Items.Insert(ConsoleOutput.Items.Count, _res.CurrentResult);
            Border border = (Border)VisualTreeHelper.GetChild( ConsoleOutput, 0 );
            ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild( border, 0 );
            scrollViewer.ScrollToBottom();
            _res.Dispose();

        }

        private void ClearConsole_Click( object sender, RoutedEventArgs e )
        {
            ConsoleOutput.Items.Clear();
        }

        private void ClearCode_Click( object sender, RoutedEventArgs e )
        {
            ConsoleInput.Clear();
        }
        
            private void LoadYodiiSyntax()
            {
                System.IO.StreamReader s = new System.IO.StreamReader( @"../../../ys.xshd" );
                {
                    using( XmlTextReader reader = new XmlTextReader( s ) )
                    {
                        ConsoleInput.SyntaxHighlighting = HighlightingLoader.Load( reader, HighlightingManager.Instance );
                    }      
                }
            }

            void ConsoleClose_Click( object sender, RoutedEventArgs e )
            {
                _root.StartConsole_Click( this, new RoutedEventArgs());
            }

            void SpecialKeyHandler( object sender, KeyEventArgs e )
            {
                if( (Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.Enter) )
                {
                    ExecuteConsole_Click( this, new RoutedEventArgs() );
                }
            }

            private void DisableSelection( object sender, SelectionChangedEventArgs e )
            {
                ConsoleOutput.Focusable = false;
                ConsoleOutput.SelectedIndex = 0;
            }

            
        }
    }

