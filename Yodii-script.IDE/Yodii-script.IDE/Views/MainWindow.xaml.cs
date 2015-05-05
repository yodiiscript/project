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

namespace Yodii_script.IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<object> l = new List<object>();
        public MainWindow()
        {
            InitializeComponent();
            LoadIDEConfig();
        }

        private void LoadIDEConfig()
        {
            this.Background = new SolidColorBrush( Colors.LightGray );
            this.ScriptCol.Background = new SolidColorBrush( Colors.Black );
            this.ScriptCol.Foreground = new SolidColorBrush( Colors.White );
        }

    }
}
