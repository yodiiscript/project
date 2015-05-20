using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Yodii_script.IDE.View_Models
{
    public class ScriptContext : INotifyPropertyChanged
    {
        ScriptList _scriptList = new ScriptList();

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
        public void RemoveByName( string name )
        {
            _scriptList.Remove( _scriptList.First( s => s.Name == name ) );
        }

        public bool CheckIfExists( Script script )
        {
            return _scriptList.Contains( script );
        }

        public ScriptList ScriptList
        {
            get { return _scriptList; }
            private set { _scriptList = value; }
        }
       
    }
}
