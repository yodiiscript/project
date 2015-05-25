using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                
                ScriptSerializer.Load(_scriptCon);
                return _scriptCon.ScriptList;
            }
        }
        public MainWindowVM()
        {
            //testButtonCommand = new RelayCommand(ShowMessage, param => this.canExecute);
            //toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
            //addScriptCommand = new RelayCommand( AddScript, CanExecuteSave );
            deleteScriptCommand = new RelayCommand(DeleteScript);
        }
        public ICommand DeleteScriptCommand
        {
            get { return deleteScriptCommand; }
        }
        private void DeleteScript(object arg)
        {
            if( arg != null )
            {
                int idx = (int)arg;
                _scriptCon.Remove( _scriptCon.ScriptList[idx].Name );
            }
        }
    }
}
