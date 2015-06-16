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

            ScriptContext context = new ScriptContext();
            Script script = context.CreateAndRegisterScript( "coucou", "ys", "trash script", "let x;" );

            Assert.That( context.ScriptList.Count == 1 );
            Assert.That( context.ScriptList[0] == script );

            ScriptContext context2 = new ScriptContext();
            context2.Load();
            Assert.That( context2.ScriptList.Count == 1 );
            Assert.That( context2.ScriptList[0].HasSameValuesAs(script) );
        }
        [Test]
        public void serialize_script_works_as_intended()
        {
            string path = "../../Models/Scripts.xml";
            File.Delete( path );
            ScriptContext context = new ScriptContext();
            Script script = context.CreateAndRegisterScript( "coucou", "ys", "trash script", "let x;" );
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

            Script script1 = context.CreateAndRegisterScript( "coucou", "ys", "script1", "let a;" );
            Script script2 = context.CreateScript( "haha", "py", "script2", "let b;" );

            context.AddScript( script1 );

            File.Copy(path,path1);

            context.AddScript( script2 );
            context.Remove( script2.Name );

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
            context.AddScript( scriptBase );

            Script scriptUpdate = context.CreateScript( "salut", "ys", "helloscript", "print('helloworld');" );
            context.Update( scriptBase.Name, scriptUpdate );

            ScriptSerializer.Load( context );
            
            Assert.That( scriptUpdate.Name == context.ScriptList[0].Name );
        }
    }

        
}
