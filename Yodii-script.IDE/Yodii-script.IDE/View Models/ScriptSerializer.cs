using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Yodii_script.IDE.View_Models
{
    /// <summary>
    /// Basic class for script serialization as XML
    /// </summary>
    public  static class ScriptSerializer
    {
        /// <summary>
        /// the save path, will be changed  whenon yodii/ck
        /// </summary>
        static string _path = "../../Models/Scripts.xml";

        /// <summary>
        /// Adds the ecified <see cref="Script"/> to the doc
        /// </summary>
        /// <param name="script">The script to save</param>
        internal static void Add( Script script )
        {
            XDocument doc = FindOrCreateFile();  
            XElement s = new XElement( "Script" );
            s.Add( new XAttribute( "name", script.Name ) );
            s.Add( new XElement( "language", script.Language ) );
            s.Add( new XElement( "description", script.Description ) );
            s.Add( new XElement( "sourceCode", script.SourceCode ) );
            doc.Element( "Scripts" ).Add( s );
            doc.Save( _path );     
        }
        /// <summary>
        /// Removes the specified <see cref="Script"/> from the document
        /// </summary>
        /// <param name="script">The script to remove</param>
        internal static void Remove( Script script )
        {            
            XDocument doc = FindOrCreateFile();

            doc.Elements( "Scripts" ).Elements("Script").First( e => (string)e.Attribute( "name" ) == script.Name ).Remove();           
            doc.Save( _path );           
        }
        /// <summary>
        /// Updates a <see cref="Script "/>
        /// </summary>
        /// <param name="name">the name of the script to replace</param>
        /// <param name="script">the new script</param>
        internal static void Update( string name, Script script )
        {
            XDocument doc = FindOrCreateFile();
            XElement s = new XElement( "Script" );
            s.Add( new XAttribute( "name", script.Name ) );
            s.Add( new XElement( "language", script.Language ) );
            s.Add( new XElement( "description", script.Description ) );
            s.Add( new XElement( "sourceCode", script.SourceCode ) );

            XElement target = doc.Elements( "Scripts" ).Elements("Script").First( e => e.Attribute( "name" ).Value == name );
            target.ReplaceWith( s );
            doc.Save( _path );
        }
        /// <summary>
        /// Loads the context scriptlist from the xml doc
        /// </summary>
        /// <param name="context">the cotext with the list to load</param>
      
        public static void Load(ScriptContext context)
        {
            XDocument doc = FindOrCreateFile();

            foreach( XElement Xscript in doc.Elements("Scripts").Elements())
            {
                Script s = context.CreateScript((string)Xscript.Attribute( "name" ), (string)Xscript.Element( "language" ), (string)Xscript.Element( "description" ), (string)Xscript.Element( "sourceCode" ));
                context.ScriptList.Add( s );
            }            
        }
        /// <summary>
        /// Ensures that the Xdocuement is available and created
        /// </summary>
        /// <returns>The Xdocument needed</returns>
        private static XDocument FindOrCreateFile()
        {          
            if( File.Exists( _path ) )
            {
                return XDocument.Load( _path );
            }
            else
            {
                XDocument d = new XDocument();
                d.Add(new XElement("Scripts"));
                return d;
            }
        }
    }
}
