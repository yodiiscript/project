using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Document;

namespace Yodii_script.IDE.View_Models
{
    class MainWindowVM
    {
        private ICommand addScriptCommand;
        private ICommand deleteScriptCommand;
        private ICommand newScriptCommand;
        private ICommand editScriptCommand;
        ScriptContext _scriptCon = new ScriptContext();
        public ScriptList Source
        {
            get
            {

                ScriptSerializer.Load( _scriptCon );
                return _scriptCon.ScriptList;
            }
        }
        public MainWindowVM()
        {
            //testButtonCommand = new RelayCommand(ShowMessage, param => this.canExecute);
            //toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
            //addScriptCommand = new RelayCommand( AddScript, CanExecuteSave );
            editScriptCommand = new RelayCommand( EditScript );
            addScriptCommand = new RelayCommand( AddScript );
            deleteScriptCommand = new RelayCommand( DeleteScript );
        }
        public ICommand DeleteScriptCommand
        {
            get { return deleteScriptCommand; }
        }
        public ICommand AddScriptCommand
        {
            get { return addScriptCommand; }
        }
        public ICommand EditScriptCommand
        {
            get { return editScriptCommand; }
        }
        private void DeleteScript( object arg )
        {
            if( arg != null )
            {
                int index = (int)arg;
                _scriptCon.Remove( _scriptCon.ScriptList[index].Name );
            }
        }
        private void AddScript( object obj )
        {
            object[] args = (object[])obj;
            string name = (string)args[0];
            object doc = args[1];
            TextDocument d = (TextDocument)doc;
            string sourceCode = d.Text;

            if( !String.IsNullOrEmpty( sourceCode ) )
            {
                Script script = _scriptCon.CreateScript( name, "ys", "trash script", sourceCode );

                if( !_scriptCon.Exists( name ) )
                {
                    _scriptCon.AddScript( script );
                }
                else
                {
                    MessageBoxResult overwrite = MessageBox.Show( "A script with this name exists \n want to overwrite ?", "Script found", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No );
                    if( overwrite == MessageBoxResult.Yes )
                    {
                        _scriptCon.Update( script.Name, script );
                    }
                }
            }
        }
        private void EditScript( object obj )
        {

        }
    }
}
