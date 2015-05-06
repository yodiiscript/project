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
        public MainWindow()
        {
            InitializeComponent();
            LoadIDEConfig();
            Script x = new Script("lol","ys","script doing nothing","let x = 5;");
            this.ScriptCol.ItemsSource = Script._scriptList;
        }

        private void LoadIDEConfig()
        {
            this.Background = new SolidColorBrush( Colors.LightGray );
            this.ScriptCol.Background = new SolidColorBrush( Colors.Black );
            this.ScriptCol.Foreground = new SolidColorBrush( Colors.White );
            //this.ScriptCol.DataContext = View_Models.Script._scriptList; DONE IN XAML
        }

    }
}
