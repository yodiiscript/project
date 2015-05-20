using NUnit.Framework;
using Yodii_script.IDE.View_Models;

namespace Yodii_script.IDE.Tests
{
    [TestFixture]
    public class ScriptTests 
    {
        [Test]
        public void script_creation_returns_proper_script()
        {
            ScriptContext context = new ScriptContext();
            Script script = context.CreateScript( "myTest", "ys", "This is a test script", "let x = 5;" );
            context.AddScript( script );

            Assert.That( script.Name == "myTest" && script.SourceCode == "let x = 5;" );
            Assert.That( context.ScriptList[0] == script );           
        }
        [Test]
        public void script_comparaisons()
        {
            ScriptContext context = new ScriptContext();
            Script script = context.CreateScript( "myTest", "ys", "This is a test script", "let x = 5;" );
            context.AddScript( script );

            Script script2 = context.CreateAndRegisterScript( "myTest", "ys", "This is a test script", "let x = 5;" );
            Script script3 = context.CreateAndRegisterScript( "myTest1", "ys", "This is a test script", "let x = 5;" );
            Script script4 = context.CreateAndRegisterScript( "myTest", "ys1", "This is a test script", "let x = 5;" );
            Script script5 = context.CreateAndRegisterScript( "myTest", "ys", "This is a test script1", "let x = 5;" );
            Script script6 = context.CreateAndRegisterScript( "myTest", "ys", "This is a test script", "let x = 5;1" );

            Assert.That( script.HasSameValuesAs( script2 ) );
            Assert.That( !script.HasSameValuesAs( script3 ) );
            Assert.That( !script.HasSameValuesAs( script4 ) );
            Assert.That( !script.HasSameValuesAs( script5 ) );
            Assert.That( !script.HasSameValuesAs( script6 ) );

            
        }
        [Test]
        public void script_create_and_register_returns_proper_script_and_works()
        {
            ScriptContext context = new ScriptContext();
            Script script = context.CreateScript( "myTest", "ys", "This is a test script", "let x = 5;" );                            

            Script script2 = context.CreateAndRegisterScript( "myTest", "ys", "This is a test script", "let x = 5;" );
            Assert.That( script2.Name == "myTest" && script2.SourceCode == "let x = 5;" );

            Assert.That( context.ScriptList[0] == script2 );
            Assert.That( context.ScriptList[0].HasSameValuesAs(script2) );
        }
        [Test]
        public void ScriptContext_contains_scripts()
        {
            ScriptContext context = new ScriptContext();

            Script script1 = context.CreateScript( "hisTest", "ys", "Another test", "let x; let y;" );
            context.AddScript( script1 );
            Script script2 = context.CreateAndRegisterScript( "myTest", "ys", "This is a test script", "let x = 5;" );

            Assert.That( context.ScriptList.Contains( script1 ) && context.ScriptList.Contains( script2 ) );
        }

        [Test]
        public void ScriptList_contains_script_at_right_index_in_list()
        {
            ScriptContext context = new ScriptContext();

            Script script = context.CreateScript( "t", "ys", "trivialscript", "let a = 2; let b = 4; let c = a+b;" );
            context.AddScript( script );

            Script script2 = context.CreateAndRegisterScript( "myTest", "ys", "This is a test script", "let x = 5;" );

            Assert.That( context.ScriptList[0] == script );
            Assert.That( context.ScriptList[1] == script2 );
        }

        [Test]
        public void setting_ScriptList_IsToggled_to_true()
        {
            ScriptContext context = new ScriptContext();
            Assert.That( context.ScriptList.IsToggled == false );
            context.ScriptList.IsToggled = true;
            Assert.That( context.ScriptList.IsToggled == true );
            context.ScriptList.IsToggled = false;
            Assert.That( context.ScriptList.IsToggled == false );

        }
    }
}
