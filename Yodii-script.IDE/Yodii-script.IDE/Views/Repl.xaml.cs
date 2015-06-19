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

namespace Yodii_script.IDE.Views
{
    /// <summary>
    /// Interaction logic for Repl.xaml
    /// </summary>
    public partial class Repl : UserControl
    {
        List<object> _output; 
        public Repl( MainWindow root )
        {
            this.Height = 500;
            
            InitializeComponent();
            ConsoleInput.LineNumbersForeground = new SolidColorBrush( Colors.Yellow );
            ConsoleInput.WordWrap = true;
            ConsoleInput.Background = new SolidColorBrush( Colors.Black );
            ConsoleInput.Foreground = new SolidColorBrush( Colors.White );
            ConsoleOutput.Background = new SolidColorBrush( Colors.Black );
            LoadYodiiSyntax();
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

