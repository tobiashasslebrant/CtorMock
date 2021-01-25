using System.Dynamic;
using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
    public class When_New_and_replacing_ctor_param : Arrange
    {
        private TestClass1 _result;

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

        public When_New_and_replacing_ctor_param() 
            => _result = Subject.New<TestClass1>(("str2", "replaced string"));

        [Fact]
        public void Will_replace_str2()
            => Assert.Equal("replaced string", _result.Str2);
        
        [Fact]
        public void Will_not_replace_str1()
            => Assert.Null(_result.Str1);
        
        [Fact]
        public void Will_not_replace_str3()
            => Assert.Null(_result.Str3);
    }
    
    public class When_New_and_replacing_ctor_param2 : Arrange
    {
        private TestClass1 _result;

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

        public When_New_and_replacing_ctor_param2() 
            => _result = Subject.New<TestClass1>(("str2", "replaced string"));

        [Fact]
        public void Will_replace_str2()
            => Assert.Equal("replaced string", _result.Str2);
        
        [Fact]
        public void Will_not_replace_str1()
            => Assert.Null(_result.Str1);
        
        [Fact]
        public void Will_not_replace_str3()
            => Assert.Null(_result.Str3);
    }
  
}