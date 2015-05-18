using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;


namespace Yodii_script.IDE.View_Models
{
    public class Script : INotifyPropertyChanged
    {
        string _name; 
        string _language; 
        string _description; 
        string _sourceCode;

        internal Script( string name, string language, string description, string sourceCode )
        {
            _name = name;
            _language = language;
            _description = description;
            _sourceCode = sourceCode;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged( [CallerMemberName] string name = null )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( name ) );
            }

        }

        #region Get-Set
        public string FullName
        {
            get { return String.Format( "{0}.{1}", _name, _language ); }
        }

        public string Name
        {
            get { return _name; }
            set 
            { 
                if( _name != value )
                {
                    _name = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged( "FullName" );
                }
            }
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
            set
            {
                if( _sourceCode != value )
                {
                    _name = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged( "SourceCode" );
                }
            }
        }
        #endregion
    }

    public class ScriptList : ObservableCollection<Script>
    {
        bool _isToggled;

        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if( _isToggled != value )
                {
                    _isToggled = value;
                    OnPropertyChanged( new PropertyChangedEventArgs( "isToggled" ) );
                }
            }
        }
    }
 

}
