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

        public MainWindow()
        {
            InitializeComponent();
            LoadYodiiSyntax();
            LoadEditorConfig();
        }


        private void button_addScript_Click( object sender, RoutedEventArgs e )
        {
            if( !String.IsNullOrEmpty( ScriptEditor.Text ) )
            {
                Script script = _scriptCon.CreateScript( entry_ScriptName.Text, "ys", "trash script", ScriptEditor.Text );

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
