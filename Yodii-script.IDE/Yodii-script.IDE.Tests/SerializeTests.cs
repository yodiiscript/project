using System.IO;
using NUnit.Framework;
using Yodii_script.IDE.View_Models;



namespace Yodii_script.IDE.Tests
{   
    [TestFixture]
    class SerializeTests
    {
        [Test]
        public void load_script_from_xml()
        {
            string path = "../../Models/Scripts.xml";
            File.Delete( path );
            ScriptContext s = new ScriptContext();
            Script script = s.CreateScript( "coucou", "ys", "trash script", "let x;" );
            s.AddScriptToList( script );
            ScriptSerializer.AddScript( script );
            ScriptList sut = ScriptSerializer.LoadScriptList();
            Assert.That( sut.Count!= 0 );
        }
        [Test]
        public void serialize_script_works_as_intended()
        {
            string path = "../../Models/Scripts.xml";
            File.Delete( path );
            ScriptContext s = new ScriptContext();
            Script sc = s.CreateScript( "coucou", "ys", "trash script", "let x;" );
            ScriptSerializer.AddScript( sc );
            Assert.That( File.Exists( path ) );
        }
        /*[Test]
        public void remove_script_from_xml() 
        {
            string path1 = "../../Models/Scripts1.xml";
            string path2 = "../../Models/Scripts1.xml";


            File.Delete( path1 );
            File.Delete( path2 );
            
            ScriptContext s = new ScriptContext();
            Script script = s.CreateScript( "coucou", "ys", "trash script", "let x;" );
            ScriptSerializer sut = new ScriptSerializer();
            sut.RemoveScript( script );
            string file = System.IO.File.ReadAllText( "../../Models/Scripts.xml" );
            string file2 = System.IO.File.ReadAllText( "../../Models/Scripts2.xml" );
            Assert.That(file == file2 );
        }*/
        [Test]
        public void edit_script_to_xml()
        {
            string path = "../../Models/Scripts.xml";
            File.Delete( path );

            ScriptContext context = new ScriptContext();

            Script scriptBase = context.CreateScript( "lol", "mdr", "ffs", "rofllmao" );
            ScriptSerializer.AddScript( scriptBase );

            Script scriptUpdate = context.CreateScript( "salut", "ys", "helloscript", "print('helloworld');" );
            ScriptSerializer.EditScript( "lol", scriptUpdate );

            ScriptList scriptlist = ScriptSerializer.LoadScriptList();
            Assert.That( scriptUpdate.Name == scriptlist[0].Name );
        }
    }

        
}
