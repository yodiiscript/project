using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Yodii_script.IDE.View_Models
{
    public class ScriptContext : INotifyPropertyChanged
    {
        private readonly ScriptList _scriptList = new ScriptList();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged( [CallerMemberName] string name = null )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( name ) );
            }

        }
        
        // TODO should retrieve values from UI instead
        public Script CreateScript( string name,string language , string desc, string sourceCode )
        {
            Script script = new Script( name, language, desc, sourceCode );
            ScriptList.Add( script );
            return script;
        }

        public ScriptList ScriptList
        {
            get { return _scriptList; }
        }
       
    }
}
