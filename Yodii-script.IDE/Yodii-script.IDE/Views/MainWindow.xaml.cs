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
using Yodii_script.IDE.View_Models;

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
            Script script = _scriptCon.CreateScript( entry_ScriptName.Text, "ys", "trash script", "let x;" );
            bool exists = _scriptCon.CheckIfExists( script );
            if( exists )
            {
                MessageBox.Show( "Le script existe déjà" );
            }
            else
            {
                _scriptCon.AddScriptToList( script );
                ScriptSerializer mySerializer = new ScriptSerializer();
                mySerializer.AddScript( script );
            }
        }


    
    }
}
