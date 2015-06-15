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
        #region Properties
        ScriptContext _scriptCon = new ScriptContext();
        private ICommand addScriptCommand;
        private ICommand deleteScriptCommand;
        private ICommand editScriptCommand;
        private ICommand clearScriptCommand;
        private ICommand debugScriptCommand;
        private string _nameSource;
        private string _descriptionSource;
        private string _codeSource;
        private TextDocument _document;
        #endregion

        #region Getters and Setters
        public string NameSource
        {
            get { return _nameSource; }
            set
            {
                if( _nameSource != value )
                {
                    _nameSource = value;
                    var h = PropertyChanged;
                    if( h != null ) h( this, new PropertyChangedEventArgs( "NameSource" ) );
                }
            }
        }
        public string DescriptionSource
        {
            get { return _descriptionSource; }
            set
            {
                if( _descriptionSource != value )
                {
                    _descriptionSource = value;
                    var h = PropertyChanged;
                    if( h != null ) h( this, new PropertyChangedEventArgs( "DescriptionSource" ) );
                }
            }
        }
        public string CodeSource
        {
            get { return _codeSource; }
            set
            {
                if( _codeSource != value )
                {
                    _codeSource = value;
                    var h = PropertyChanged;
                    if( h != null ) h( this, new PropertyChangedEventArgs( "CodeSource" ) );
                }
            }
        }
        public TextDocument Document
        {
            get { return _document; }
            set
            {
                if( _document != value )
                {
                    _document = value;
                    var h = PropertyChanged;
                    if( h != null ) h( this, new PropertyChangedEventArgs( "Document" ) );
                }
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
        #endregion

        #region Icommand Getters
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
        public ICommand DebugScriptCommand
        {
            get { return debugScriptCommand; }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler  PropertyChanged;

        protected void OnPropertyChanged( string propertyName )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }
        #endregion

        #region Constructor
        public MainWindowVM()
        {
            addScriptCommand = new RelayCommand( AddScript, CanExecuteAddScript );
            editScriptCommand = new RelayCommand( EditScript, CanExecuteEditScript );
            deleteScriptCommand = new RelayCommand( DeleteScript, CanExecuteDeleteScript );
            clearScriptCommand = new RelayCommand( ClearScript, CanExecuteClearScript );
            debugScriptCommand = new RelayCommand( DebugScript, CanExecuteDebugScript );
            _document = new TextDocument();
        }
        #endregion

        #region Commands
        #region AddScript
        private void AddScript( object obj )
        {
            //object[] args = (object[])obj;
            //string name = (string)args[0];
            //object doc = args[1];
            //TextDocument d = (TextDocument)doc;
            //string sourceCode = d.Text;

            Script script = _scriptCon.CreateScript( _nameSource, "ys", _descriptionSource, _document.Text );

            if( !_scriptCon.Exists( _nameSource ) )
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
        private bool CanExecuteAddScript( object obj )
        {
            if( !String.IsNullOrEmpty( _nameSource ) && !String.IsNullOrEmpty( _descriptionSource ) && !String.IsNullOrEmpty( _document.Text ) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region ClearScript
        private void ClearScript( object obj )
        {
            NameSource = "";
            DescriptionSource = "";
            _document.Text = "";
        }
        private bool CanExecuteClearScript( object obj )
        {
            if( !String.IsNullOrEmpty( _nameSource ) || !String.IsNullOrEmpty( _descriptionSource ) || !String.IsNullOrEmpty( _document.Text ) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region EditScript
        private void EditScript( object obj )
        {
            Script s = (Script)obj;
            NameSource = s.Name;
            DescriptionSource = s.Description;
            _document.Text = s.SourceCode;
            //_scriptCon.CurrentScript = _scriptCon.ScriptList[this.ScriptCol.SelectedIndex];
            //_scriptCon.ScriptList[this.ScriptCol.SelectedIndex].IsBeingEdited = true;
        }
        private bool CanExecuteEditScript( object obj )
        {
            if( obj != null )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region DeleteScript
        private void DeleteScript( object obj )
        {
            int index = (int)obj;
            if( index >= 0 )
            {
                _scriptCon.Remove( _scriptCon.ScriptList[index].Name );
            }
        }
        private bool CanExecuteDeleteScript( object obj )
        {
            if( obj != null && (int)obj != -1 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        private void DebugScript( object obj )
        {
        }
        private bool CanExecuteDebugScript( object obj )
        {
            if( !String.IsNullOrEmpty( _document.Text ) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

