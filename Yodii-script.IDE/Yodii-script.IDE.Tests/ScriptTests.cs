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
        public void script_creation_behaves_properly()
        {
            Script sut = new Script( "myTest", "ys", "This is a test script", "let x = 5;" );
            Assert.That( sut.Name == "myTest" && sut.SourceCode =="let x = 5;");
        }

        [Test]
        public void static_script_list_stores_properly()
        {
            Script s = new Script( "myTest", "ys", "This is a test script", "let x = 5;" );
            Script sut = new Script( "another test", "ys", "test", "let a;" );
            Assert.That( Script._scriptList.Contains( s ) && Script._scriptList.Contains( sut ) );
        }
    }
}
