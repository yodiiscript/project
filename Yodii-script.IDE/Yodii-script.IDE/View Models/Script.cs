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
        public static ScriptList _scriptList;
        public String _name { get; set; }
        public String _language { get; set; }
        public String _description { get; set; }
        public String _content { get; set; }

        public Script( string name, string language, string description, string sourceCode )
        {
            _name = name;
            _language = language;
            _description = description;
            _content = sourceCode;
            _scriptList = new ScriptList( _name );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged( string name )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( name ) );
            }

        }
    }

    public class ScriptList : ObservableCollection<string>
    {
        internal bool isToggled;

        public ScriptList(string s)
        {
            Add( s );
            isToggled = false;
        }
       
    }
}
