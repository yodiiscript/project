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
        List<object> _output;
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
            ConsoleInput.Background = new SolidColorBrush( Colors.Black );
            ConsoleInput.Foreground = new SolidColorBrush( Colors.White );
            ConsoleOutput.Background = new SolidColorBrush( Colors.Black );
            //ConsoleInput.KeyUp += new KeyEventHandler( ExecuteOutput );
            ConsoleOutput.Focusable = false;
            LoadYodiiSyntax();
        }

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
            _output = new List<object>();
            _output.Add( _res.CurrentResult );
            ConsoleOutput.Items.Insert(ConsoleOutput.Items.Count, _res.CurrentResult);
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

            
        }
    }

