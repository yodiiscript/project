using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Document;

namespace Yodii_script.IDE.View_Models
{
    class MainWindowVM : INotifyPropertyChanged
    {
        private ICommand addScriptCommand;
        private ICommand deleteScriptCommand;
        //private ICommand newScriptCommand;
        private ICommand editScriptCommand;
        private ICommand clearScriptCommand;
        ScriptContext _scriptCon = new ScriptContext();
        private string nameSource;
        private string descriptionSource;
        private string codeSource;
        public string NameSource
        {
            get { return nameSource; }
            set
            {
                nameSource = value;
                if( PropertyChanged != null )
                    PropertyChanged( this,
                        new PropertyChangedEventArgs( "NameSource" ) );
            }
        }
        public string DescriptionSource
        {
            get { return descriptionSource; }
            set
            {
                descriptionSource = value;
                if( PropertyChanged != null )
                    PropertyChanged( this,
                        new PropertyChangedEventArgs( "DescriptionSource" ) );
            }
        }
        public string CodeSource
        {
            get { return codeSource; }
            set
            {
                codeSource = value;
                if( PropertyChanged != null )
                    PropertyChanged( this,
                        new PropertyChangedEventArgs( "CodeSource" ) );
            }
        }
        public event PropertyChangedEventHandler  PropertyChanged;

        protected void OnPropertyChanged( string propertyName )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }
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
            addScriptCommand = new RelayCommand( AddScript, CanExecuteAddScript );
            editScriptCommand = new RelayCommand( EditScript );
            //addScriptCommand = new RelayCommand( AddScript );
            deleteScriptCommand = new RelayCommand( DeleteScript );
            clearScriptCommand = new RelayCommand( ClearScript );
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
        public ICommand ClearScriptCommand
        {
            get { return clearScriptCommand; }
        }
        private void DeleteScript( object arg )
        {
            if( arg != null )
            {
                int index = (int)arg;
                if( index >= 0 )
                {
                    _scriptCon.Remove( _scriptCon.ScriptList[index].Name );
                }               
            }
        }
        private void AddScript2( object obj )
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
        private void AddScript( object obj )
        {
            object[] args = (object[])obj;
            string name = (string)args[0];
            object doc = args[1];
            TextDocument d = (TextDocument)doc;
            string sourceCode = d.Text;

            Script script = _scriptCon.CreateScript( nameSource, "ys", DescriptionSource, sourceCode );

            if( !_scriptCon.Exists( nameSource ) )
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
        private bool CanExecuteAddScript(object obj)
        {
            if( !String.IsNullOrEmpty( NameSource ) && !String.IsNullOrEmpty( DescriptionSource ) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void EditScript( object obj )
        {
            if( obj != null )
            {
                NameSource = obj.ToString();
            }
        }
        private void ClearScript( object obj )
        {
            NameSource = "";
            DescriptionSource = "";
        }
    }
}
