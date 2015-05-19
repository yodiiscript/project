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
        private  ScriptList _scriptList = new ScriptList();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged( [CallerMemberName] string name = null )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( name ) );
            }

        }
        
        // TODO should retrieve values from UI instead
        public Script CreateScript( string name, string language , string desc, string sourceCode )
        {
            return new Script( name, language, desc, sourceCode );
        }

        public void AddScriptToList( Script script )
        {
            _scriptList.Add( script );
        }
        public void RemoveByName( Script script )
        {
            for( int i = 0; i < _scriptList.Count; i += 1 )
            {
                if (_scriptList[i].Name == script.Name)
                {
                    _scriptList.Remove( _scriptList[i] );
                }
            }
        }

        public bool CheckIfExists( Script script )
        {
            for( int i = 0; i < _scriptList.Count; i += 1 )
            {
                if( _scriptList[i].Name == script.Name )
                {
                    return true;
                }
            }
            return false;
        }

        public ScriptList ScriptList
        {
            get { return _scriptList; }
            set { _scriptList = value; }
        }
       
    }
}
