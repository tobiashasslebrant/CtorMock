using System.Reflection;
using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_and_replacing_ctor_param : Arrange
    {
        class TestClass1
        {
            public string Str1 { get; }
            public string Str2 { get; }
            public string Str3 { get; }

            public TestClass1(string str1, string str2, string str3)
            {
                Str1 = str1;
                Str2 = str2;
                Str3 = str3;
            }
        }

        (object, bool) replaceFunction(ParameterInfo parameterInfo, int depth)
            => parameterInfo.Name == "str2"
                ? ("replaced string", true)
                : (null, false);

        [Fact]
        public void Will_replace_str2()
            => Assert.Equal("replaced string", Subject.New<TestClass1>(replaceFunction).Str2);
        
        [Fact]
        public void Will_not_replace_str1()
            => Assert.Null(Subject.New<TestClass1>(replaceFunction).Str1);
        
        [Fact]
        public void Will_not_replace_str3()
            => Assert.Null(Subject.New<TestClass1>(replaceFunction).Str3);
    }
}