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
        ScriptSerializer _scriptSer = new ScriptSerializer();

        public MainWindow()
        {
            InitializeComponent();
            LoadIDEConfig();
            this.ScriptCol.ItemsSource = _scriptCon.ScriptList;
            LoadYodiiSyntax();
            LoadEditorConfig();
        }

        private void LoadIDEConfig()
        {
            this.Background = new SolidColorBrush( Colors.LightGray );
            this.ScriptCol.Background = new SolidColorBrush( Colors.Black );
            this.ScriptCol.Foreground = new SolidColorBrush( Colors.White );
            _scriptCon.ScriptList = _scriptSer.LoadScriptList();
        }

        private void button_addScript_Click( object sender, RoutedEventArgs e )
        {
            if( !String.IsNullOrEmpty( ScriptEditor.Text ) )
            {
                Script script = _scriptCon.CreateScript( entry_ScriptName.Text, "ys", "trash script", ScriptEditor.Text );
                bool exists = _scriptCon.CheckIfExists( script );

                if( exists )
                { MessageBox.Show( "A script with this name already exists" ); }
                else
                {
                    _scriptCon.AddScriptToList( script );
                    _scriptSer.AddScript( script );
                }
            }
        }

        private void LoadYodiiSyntax()
        {
            // Load a different syntax config file
            System.IO.StreamReader s = new System.IO.StreamReader( @"C:\dev\dotnet\avalon_test\ys.xshd" );
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
            

        }

        private void button_deleteScript_Click( object sender, RoutedEventArgs e )
        {
            if( ScriptCol.SelectedItem != null)
            {
                int idx = ScriptCol.SelectedIndex;
                _scriptSer.RemoveScript( _scriptCon.ScriptList[idx] );
                _scriptCon.ScriptList.RemoveAt( idx );
            }
        }

        private void button_newScript_Click( object sender, RoutedEventArgs e )
        {
            ScriptEditor.Visibility = Visibility.Visible;
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
