using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace Yodii_script.IDE.View_Models
{
    public class Script : INotifyPropertyChanged
    {
        public static ScriptList _scriptList= new ScriptList();
        string _name; 
        string _language; 
        string _description; 
        string _sourceCode;

        public Script( string name, string language, string description, string sourceCode )
        {
            _name = name;
            _language = language;
            _description = description;
            _sourceCode = sourceCode;
            _scriptList.AddScript( this );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged( string name )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( name ) );
            }

        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string SourceCode
        {
            get { return _sourceCode; }
            set { _sourceCode = value; }
        }
        public ScriptList ScriptList
        {
            get { return _scriptList; }
        }
    }

    public class ScriptList : ObservableCollection<Script>
    {
        internal static bool _isToggled;
        internal readonly List<string> _scriptNames=new List<string>();
        public void AddScript( Script s )
        {
            Add( s );
            _scriptNames.Add( s.Name );
        }
        

    }
}
