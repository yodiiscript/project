using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Yodii_script.IDE.View_Models;

namespace Yodii_script.IDE.Tests
{
    [TestFixture]
    public class ScriptTests 
    {
        [Test]
        public void script_creation_yields_proper_script()
        {
            ScriptContext s = new ScriptContext();
            Script sut = s.CreateScript( "myTest", "ys", "This is a test script", "let x = 5;" );
            Assert.That( sut.Name == "myTest" && sut.SourceCode == "let x = 5;" );
            Assert.That(s.ScriptList[0] == sut);
        }

        [Test]
        public void ScriptList_contains_script_at_right_index_in_list()
        {
            ScriptContext s = new ScriptContext();
            Script sc = s.CreateScript( "t", "ys", "trivialscript", "let a = 2; let b = 4; let c = a+b;" );
            Script sut = s.CreateScript( "myTest", "ys", "This is a test script", "let x = 5;" );
            Assert.That( s.ScriptList[1] == sut );
        }

        [Test]
        public void ScriptContext_contains_scripts()
        {
            ScriptContext s = new ScriptContext();
            Script sc = s.CreateScript( "hisTest", "ys", "Another test", "let x; let y;" ); 
            Script sut = s.CreateScript( "myTest", "ys", "This is a test script", "let x = 5;" );
            Assert.That( s.ScriptList.Contains( sut ) && s.ScriptList.Contains( sc ) );
        }

        [Test]
        public void setting_ScriptList_IsToggled_to_true()
        {
            ScriptContext sut = new ScriptContext();
            sut.ScriptList.IsToggled = true;
            Assert.That( sut.ScriptList.IsToggled == true );
        }
    }
}
