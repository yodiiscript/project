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
        [Test]
        public void remove_script_from_xml() 
        {
            string path = "../../Models/Scripts.xml";
            string path1 = "../../Models/Scripts1.xml";


            File.Delete( path );
            File.Delete( path1 );
            
            ScriptContext context = new ScriptContext();

            Script script1 = context.CreateScript( "coucou", "ys", "script1", "let a;" );
            Script script2 = context.CreateScript( "haha", "py", "script2", "let b;" );

            context.AddScriptToList( script1 );
            ScriptSerializer.AddScript( script1 );
            File.Copy(path,path1);

            context.AddScriptToList( script2 );
            ScriptSerializer.AddScript( script2 );
            ScriptSerializer.RemoveScript( script2 );
            context.RemoveByName( script2.Name );

            string file = System.IO.File.ReadAllText( path );
            string file2 = System.IO.File.ReadAllText( path1 );
            Assert.That(file == file2 );
        }
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
