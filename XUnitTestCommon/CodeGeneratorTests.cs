using Common.CodeGenerator;
using System;
using Xunit;

namespace XUnitTestCommon
{
    public class CodeGeneratorTest
    {
        [Fact]
        public void GenerateTest()
        {
            CodeGenerator.Generate();
        }
    }
}
