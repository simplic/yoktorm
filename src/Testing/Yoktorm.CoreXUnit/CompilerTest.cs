using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Yoktorm.CoreXUnit
{
    public static class CompilerTest
    {
        [Fact]
        public static void _Do()
        {
            var compiler = new Compiler.ModelCompiler
                (
                    new Compiler.CompilerConfiguration()
                    {
                        AssemblyName = "YTest",
                        Namespace = "YTest"
                    }
                );

            var structure = new Db.DatabaseStructure();
            var asm = compiler.Compile(structure);

            var assembly = Assembly.Load(asm);
            var type = assembly.GetType("_YTest.YTest");
            
            dynamic inst = Activator.CreateInstance(type);
            
            Assert.Equal("Yoktorm!", inst.Test());
        }
    }
}
