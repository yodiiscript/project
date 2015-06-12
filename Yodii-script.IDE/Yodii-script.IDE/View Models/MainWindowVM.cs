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
                //ScriptSerializer.Load(_scriptCon);
                return _scriptCon.ScriptList;
            }
        }
    }
}
