using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Yodii_script.IDE.View_Models;
using System.IO;



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
            ScriptSerializer ser = new ScriptSerializer();
            ScriptContext s = new ScriptContext();
            Script script = s.CreateScript( "coucou", "ys", "trash script", "let x;" );
            s.AddScriptToList( script );
            ser.AddScript( script );
            ScriptList sut = ser.LoadScriptList();
            Assert.That( sut.Count!= 0 );
        }
        [Test]
        public void serialize_script_works_as_intended()
        {
            string path = "../../Models/Scripts.xml";
            File.Delete( path );
            ScriptContext s = new ScriptContext();
            Script sc = s.CreateScript( "coucou", "ys", "trash script", "let x;" );
            ScriptSerializer sut = new ScriptSerializer();
            sut.AddScript( sc );
            Assert.That( File.Exists( path ) );
        }
        [Test]
        public void remove_script_from_xml() 
        {
            string path = "../../Models/Scripts.xml";
            File.Delete( path );
            serialize_script_works_as_intended();
            ScriptContext s = new ScriptContext();
            Script sc = s.CreateScript( "coucou", "ys", "trash script", "let x;" );
            ScriptSerializer sut = new ScriptSerializer();
            sut.RemoveScript( sc );
            string file = System.IO.File.ReadAllText( "../../Models/Scripts.xml" );
            string file2 = System.IO.File.ReadAllText( "../../Models/Scripts2.xml" );
            Assert.That(file == file2 );
        }
        [Test]
        public void edit_script_to_xml()
        {
            string path = "../../Models/Scripts.xml";
            File.Delete( path );
            ScriptContext s = new ScriptContext();
            ScriptSerializer sut = new ScriptSerializer();
            Script scr = s.CreateScript("lol","mdr","ffs","rofllmao");
            Script sc = s.CreateScript( "salut", "ys", "helloscript", "print('helloworld');" );
            sut.AddScript( scr );
            sut.EditScript( "lol", sc );
            ScriptList scriptlist = sut.LoadScriptList();
            Assert.That( sc.Name == scriptlist[0].Name);
        }
    }

        
}
