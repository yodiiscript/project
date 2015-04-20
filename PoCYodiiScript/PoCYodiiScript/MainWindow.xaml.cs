using System.Windows;
using System;
using DynamicLua;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
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
        CalculatorOutput output;
        CalculatorInput input;

        public CalculatorOutput MyOutput { get { return output; } }

        public MainWindow()
        {
            InitializeComponent();
            output = new CalculatorOutput();
            input = new CalculatorInput();
            this.DataContext = this;
        }

        private void add_Click( object sender, RoutedEventArgs e )
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            this.input.Value1 = this.input1.Text;
            this.input.Value2 = this.input2.Text;
            scope.SetVariable( "input", this.input );
            scope.SetVariable( "output", this.output );
            string pySrc =   "output.SetResult(str(int(input.Value1)+int(input.Value2)))";
            engine.Execute( pySrc, scope );
        }

        private void rem_Click( object sender, RoutedEventArgs e )
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            this.input.Value1 = this.input1.Text;
            this.input.Value2 = this.input2.Text;
            scope.SetVariable( "input", this.input );
            scope.SetVariable( "output", this.output );
            string pySrc =   "output.SetResult(str(int(input.Value1)-int(input.Value2)))";
            engine.Execute( pySrc, scope );
        }

        private void mul_Click( object sender, RoutedEventArgs e )
        {
            input.Value1 = input1.Text;
            input.Value2 = input2.Text;

            dynamic lua = new DynamicLua.DynamicLua();
            string luaInput = System.IO.File.ReadAllText( "../../add.lua" );
            lua( luaInput );
            int n1 = Convert.ToInt32( this.input1.Text );
            int n2 = Convert.ToInt32( this.input2.Text );
            dynamic r = lua.multiply( n1, n2 );
            this.result.Content = r.ToString();
        }

        private void div_Click( object sender, RoutedEventArgs e )
        {
            input.Value1 = input1.Text;
            input.Value2 = input2.Text;

            var engine = new ActiveScriptEngine( "VBScript" );

            engine.AddObject( "input", input );
            engine.AddObject( "output", output );
            

            engine.AddCode( "output.SetResult(CStr(CInt(input.Value1) / CInt(input.Value2)))");
            
            engine.Start();
            engine.Dispose();
        }

        private void pow_Click( object sender, RoutedEventArgs e )
        {
            input.Value1 = input1.Text;
            input.Value2 = input2.Text;

            var engine = new ActiveScriptEngine( "JScript" );
            engine.AddObject( "input", input );
            engine.AddObject( "output", output );
            engine.AddCode( "output.SetResult(Math.pow(parseInt(input.Value1), parseInt(input.Value2)).toString());" );

            engine.Start();
            engine.Dispose();
            
        }
    }
}
