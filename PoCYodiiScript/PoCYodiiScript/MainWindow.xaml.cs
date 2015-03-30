using System.Windows;
using System;
using DynamicLua;
using IronPython;
using System.Text;
using ActiveXScriptLib;
using System.Runtime.InteropServices;


namespace PoCYodiiScript
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void add_Click( object sender, RoutedEventArgs e )
        {
            var n1 =  Convert.ToInt32(this.input1.Text);
            var n2 =  Convert.ToInt32(this.input2.Text);
            var pySrc = @"def add(a,b):
                            return a+b";
            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope  = engine.CreateScope();
            engine.Execute( pySrc, scope );
            var add =  scope.GetVariable( "add" );
            var sum = add( n1, n2 );
            this.result.Text =  sum.ToString();
        }

        private void rem_Click( object sender, RoutedEventArgs e )
        {
            var n1 =  Convert.ToInt32( this.input1.Text );
            var n2 =  Convert.ToInt32( this.input2.Text );
            var ironRubyRuntime = IronRuby.Ruby.CreateRuntime();
            dynamic loadIRuby = ironRubyRuntime.UseFile( "../../substract.rb" );
            this.result.Text = loadIRuby.substract( n1, n2 ).ToString(); 
        }

        private void mul_Click( object sender, RoutedEventArgs e )
        {
            dynamic lua = new DynamicLua.DynamicLua();
            string luaInput = System.IO.File.ReadAllText( "../../add.lua" );
            lua( luaInput );
            int n1 = Convert.ToInt32( this.input1.Text );
            int n2 = Convert.ToInt32( this.input2.Text );
            dynamic r = lua.multiply( n1, n2 );
            this.result.Text = r.ToString();
        }

        private void div_Click( object sender, RoutedEventArgs e )
        {
            var engine = new ActiveScriptEngine( "VBScript" );

            engine.AddCode( "Public Function Div(a, b) " +
                           "   Div = a / b " +
                           "End Function" );

            engine.Start();

            dynamic script = engine.GetScriptHandle();
            double res = script.Div( Convert.ToDouble( input1.Text ), Convert.ToDouble( input2.Text ) );
            result.Text = res.ToString();
        }

        private void pow_Click( object sender, RoutedEventArgs e )
        {
            //todo JS
        }
    }
}
