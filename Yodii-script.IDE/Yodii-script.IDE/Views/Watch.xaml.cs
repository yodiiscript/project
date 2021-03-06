﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Yodii.Script;
using Yodii.Script.Debugger;
using Yodii_script.IDE;


namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlGUI.xaml
    /// </summary>
    public partial class Watch : UserControl
    {
        ObservableCollection<VarData> _varsCollection = new ObservableCollection<VarData>();
        ScriptEngineDebugger _engine = new ScriptEngineDebugger( new GlobalContext() );
        ScriptEngine.Result _res;
        MainWindow _root;
    public Watch( MainWindow root)
        {
            InitializeComponent(); 
            _root = root;
            string script = root.ScriptEditor.Text;

            Expr exp = ExprAnalyser.AnalyseString( script );

            BreakableVisitor bkv = new BreakableVisitor();
            bkv.VisitExpr( exp );
            for( int i = 0; i < root.BreakPointsMargin.Items.Count; i++ )
            {
                if( (bool)((CheckBox)root.BreakPointsMargin.Items[i]).IsChecked )
                {
                    _engine.Breakpoints.AddBreakpoint( bkv.BreakableExprs[i][0] );
                }
            }

            _res = _engine.Execute( exp );

             if( !_res.CanContinue )
             {
                 _res.Dispose();
                 _continue.IsEnabled = false;
                 root.DebugPanel.Children.Remove(this); 
                
             }       
         
    }

    public ObservableCollection<VarData> VarsCollection
    { get { return _varsCollection; } }

    private void add_Click( object sender, RoutedEventArgs e )
    {        
        var Test = _engine.ScopeManager.FindByName( addVars.Text );
        if( Test == null )
        {
            _varsCollection.Add( new VarData
            {
                VarName = addVars.Text,
                VarValue = "var doesn't exist",
                VarType = "var doesn't exist",
            } );
        }
        else
        {
            _varsCollection.Add( new VarData
            {
                VarName = addVars.Text,
                VarValue = EvalTokenizerDebugger.Escape(Test.Object.ToString()),
                VarType = Test.Object.Type.ToString(),
            } );
        }
    }
    private void clearButton_Click( object sender, RoutedEventArgs e )
    {
        _varsCollection.Clear();                
    }
  
    private void TextBox_KeyDown( object sender, System.Windows.Input.KeyEventArgs e )
    {
        if( e.Key == System.Windows.Input.Key.Enter )
        {            
            TextBox tb = (TextBox)sender;
            var a = tb.Text;

            GUI.VarData obj2 = tb.DataContext as GUI.VarData;
            RefRuntimeObj O = _engine.ScopeManager.FindByName( obj2.VarName ).Object;
           
            RuntimeObj result;
            bool error;
           
            error = EvalTokenizerDebugger.TryParse(_engine.Context, a, out result);
            if( result == null && error == true )
            {
                MessageBoxResult popUp = MessageBox.Show( "Invalid Input" );
                tb.Text = obj2.VarValue;                               
            } 
            else if (result != null)
            {              
                O.Value = result;
                obj2.Refresh(_engine);

            }
        }           
    }

    private void _continue_Click( object sender, RoutedEventArgs e )
    {
        
        if( !_res.CanContinue )
        {
            _res.Dispose();
            _root.Debug_Click( sender, e );
 
        }
        else
        {
            _res.Continue();
            foreach (VarData v in _varsCollection)
            {
                v.Refresh(_engine);
            }
            _continue.IsEnabled = _res.CanContinue;
        }              
    }
    private void StopDebbuging_Click(object sender, RoutedEventArgs e)
    {
        _res.Dispose();
        _root.Debug_Click( sender, e );
    }

    private void BreakAlways_Click( object sender, RoutedEventArgs e )
    {
        _engine.Breakpoints.BreakAlways = !_engine.Breakpoints.BreakAlways;
    }

    private void addVars_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
    {
        this.addVars.Text = "";
    }

    private void addVars_KeyDown( object sender, System.Windows.Input.KeyEventArgs e )
    {
        if( e.Key == System.Windows.Input.Key.Enter ) add_Click( sender, e );
    }
}  
  public class VarData : INotifyPropertyChanged
  {
      string _name;
      string _type;
      string _value;
      public string VarName
      {
          get { return _name; }
          set { _name = value; }
      }
    public string VarType { 
        get{return _type;}
        set{ _type = value;
        RaisePropertyChanged( "VarType" );           
            }
    }
    public string VarValue { 
        get{return _value;}
        set{_value = value;
            RaisePropertyChanged("VarValue");
        }
    }
    public void Refresh(ScriptEngineDebugger engine)
    {
        var Test = engine.ScopeManager.FindByName( this.VarName );
        if (Test != null)
        {
            this.VarValue = EvalTokenizerDebugger.Escape(Test.Object.ToString());
            this.VarType = Test.Object.Type.ToString();
        }
    }

    #region INotifyPropertyChanged Members
    protected void RaisePropertyChanged( [CallerMemberName] string name = null )
    {
        if( PropertyChanged != null )
        {
            PropertyChanged( this, new PropertyChangedEventArgs( name ) );
        }

    }
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }      
}
