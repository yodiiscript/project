using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Yodii_script.IDE.View_Models
{
    public class ScriptSerializer
    {
        public void AddScript( Script script )
        {
            string path = "../../Models/Scripts.xml";
            try
            {
                XDocument doc = XDocument.Load( path );
            }
            catch( FileNotFoundException )
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration( "1.0", "UTF-8", null );
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore( xmlDeclaration, root );
                XmlElement element = doc.CreateElement( string.Empty, "Scripts", string.Empty );
                doc.AppendChild( element );
                doc.Save( path );
            }
            finally
            {
                XDocument doc = XDocument.Load( path );
                XElement root = new XElement( "Script" );
                root.Add( new XAttribute( "name", script.Name ) );
                root.Add( new XElement( "language", script.Language ) );
                root.Add( new XElement( "description", script.Description ) );
                root.Add( new XElement( "sourceCode", script.SourceCode ) );
                doc.Element( "Scripts" ).Add( root );
                doc.Save( path );
            }
        }

        public void RemoveScript( Script script )
        {
            string path = "../../Models/Scripts.xml";
            try
            {
                XDocument doc = XDocument.Load( path );
                doc.Descendants( "Script" ).Where( x => (string)x.Attribute( "name" ) == script.Name ).Remove();
                doc.Save( path );
            }
            catch( FileNotFoundException )
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration( "1.0", "UTF-8", null );
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore( xmlDeclaration, root );
                XmlElement element = doc.CreateElement( string.Empty, "Scripts", string.Empty );
                doc.AppendChild( element );
                doc.Save( path );
            }
        }

        public void EditScript( string name, Script script )
        {
            string path = "../../Models/Scripts.xml";
            try
            {
                XDocument doc = XDocument.Load( path );
                var target = doc
                            .Element( "Scripts" )
                            .Elements( "Script" )
                            .Where( e => e.Attribute( "name" ).Value == name )
                            .Single();

                target.Attribute( "name" ).Value = script.Name;
                target.Element( "language" ).Value = script.Language;
                target.Element( "description" ).Value = script.Description;
                target.Element( "sourceCode" ).Value = script.SourceCode;
                doc.Save( path );
            }
            catch( FileNotFoundException )
            {
                AddScript( script );
            }
            catch( InvalidOperationException )
            {
                AddScript( script );
            }
        }

        public ScriptList LoadScriptList()
        {
            string path = "../../Models/Scripts.xml";
            ScriptContext s = new ScriptContext();
            try
            {
                var scripts = from c in XElement.Load( path ).Elements( "Script" )
                              select new
                              {
                                  name = c.Attribute( "name" ).Value,
                                  language = c.Element( "language" ).Value,
                                  description = c.Element( "description" ).Value,
                                  sourceCode = c.Element( "sourceCode" ).Value,
                              };
                foreach( var script in scripts )
                {
                    Script scr = s.CreateScript( script.name, script.language, script.description, script.sourceCode );
                    s.AddScriptToList( scr );
                }
            }
            catch( FileNotFoundException )
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration( "1.0", "UTF-8", null );
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore( xmlDeclaration, root );
                XmlElement element = doc.CreateElement( string.Empty, "Scripts", string.Empty );
                doc.AppendChild( element );
                doc.Save( path );
            }
            return s.ScriptList;
        }
    }
}
