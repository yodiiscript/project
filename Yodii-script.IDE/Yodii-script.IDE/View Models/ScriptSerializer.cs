using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Yodii_script.IDE.View_Models
{
    public static class ScriptSerializer
    {
        static string _path = "../../Models/Scripts.xml";

        public static void AddScript( Script script )
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

        public static void RemoveScript( Script script )
        {            
            XDocument doc = FindOrCreateFile();

            doc.Elements( "Scripts" ).First( e => (string)e.Attribute( "name" ) == script.Name ).Remove();

            /*foreach( XElement s in doc.Elements("Script").Nodes() )
            {
                if( (string)s.Attribute( "name" ) == script.Name ) s.Remove();
            }*/
            doc.Save( _path );           
        }

        public static void EditScript( string name, Script script )
        {         
            XDocument doc = XDocument.Load( _path );
            XElement s = new XElement( "Script" );
            s.Add( new XAttribute( "name", script.Name ) );
            s.Add( new XElement( "language", script.Language ) );
            s.Add( new XElement( "description", script.Description ) );
            s.Add( new XElement( "sourceCode", script.SourceCode ) );


            XElement target = doc.Elements( "Scripts" ).Elements("Script").First( e => e.Attribute( "name" ).Value == name );
            target.ReplaceWith( s );
            doc.Save( _path );
        }

        public static ScriptList LoadScriptList()
        {
            ScriptContext context = new ScriptContext();
            XDocument doc = XDocument.Load( _path );

            foreach( XElement Xscript in doc.Elements("Scripts").Elements())
            {
                Script script = context.CreateScript( (string)Xscript.Attribute( "name" ), (string)Xscript.Element( "language" ), (string)Xscript.Element( "description" ), (string)Xscript.Element( "sourceCode" ) );
                context.AddScriptToList( script );
            }

            return context.ScriptList;
        }
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
