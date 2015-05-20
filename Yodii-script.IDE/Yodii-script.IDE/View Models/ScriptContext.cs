using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Yodii_script.IDE.View_Models
{
    /// <summary>
    /// Basic class for XML scrips handling
    /// </summary>
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

        /// <summary>
        /// Creates and return a new instance of <see cref="Script"/>
        /// </summary>
        /// <param name="name">Desired name for the script</param>
        /// <param name="language">the progrmming language for the script</param>
        /// <param name="desc">The description of the script</param>
        /// <param name="sourceCode">The source code to be executed</param>
        /// <returns>The created <see cref="Script"/></returns>
        public Script CreateScript( string name, string language , string desc, string sourceCode )
        {
            Script s = new Script( name, language, desc, sourceCode );            
            return s;
        }
        /// <summary>
        /// Creates, registers and return a new instance of <see cref="Script"/>
        /// </summary>
        /// <param name="name">Desired name for the script</param>
        /// <param name="language">the progrmming language for the script</param>
        /// <param name="desc">The description of the script</param>
        /// <param name="sourceCode">The source code to be executed</param>
        /// <returns>The created <see cref="Script"/></returns>
        public Script CreateAndRegisterScript( string name, string language, string desc, string sourceCode )
        {
            Script s = new Script( name, language, desc, sourceCode );
            AddScript( s );
            return s;
        }
        public void Load()
        {
            ScriptSerializer.Load( this );
        }
        /// <summary>
        /// Addes the specified <see cref="Script"/> to the list and serializer
        /// </summary>
        /// <param name="script">The <see cref="Script"/> to add</param>
        public void AddScript( Script script )
        {
            _scriptList.Add( script );
            ScriptSerializer.Add( script );
        }
        /// <summary>
        /// Removes the specified <see cref="Script"/> to the list and serializer
        /// </summary>
        /// <param name="name">The name of the<see cref="Script"/> to remove</param>
        public void Remove( string name )
        {
            ScriptSerializer.Remove( _scriptList.First( s => s.Name == name ) );
            _scriptList.Remove( _scriptList.First( s => s.Name == name ) );
        }
        /// <summary>
        /// Updates the fiels of the <see cref="Script"/> having the input name
        /// </summary>
        /// <param name="name">the name of the <see cref="Script"/> to edit</param>
        /// <param name="script"> the new script to save</param>
        public void Update(string name, Script script )
        {
            ScriptSerializer.Update( name, script );
            _scriptList[_scriptList.IndexOf(_scriptList.FirstOrDefault( s => s.Name == name ))] = script;
        }
        /// <summary>
        /// Verify existence of a script
        /// </summary>
        /// <param name="name">the name of the script</param>
        /// <returns> a boolean</returns>
        public bool Exists( string name )
        {
            return _scriptList.Contains( _scriptList.FirstOrDefault( s => s.Name == name ) );
        }
        /// <summary>
        /// Verify existence of a script
        /// </summary>
        /// <param name="name">the script</param>
        /// <returns> a boolean</returns>
        public bool Exists( Script script )
        {
            return _scriptList.Contains( script );
        }
        /// <summary>
        /// The list of scripts of the context
        /// </summary>
        public ScriptList ScriptList
        {
            get { return _scriptList; }
            private set { _scriptList = value; }
        }
       
    }
}
