using System;
using System.Reflection;
using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_and_replacing_ctor_param_with_wrong_type : Arrange
    {
        class TestClass1
        {
            public TestClass1(string str1){}
        }
        
        (object, bool) replaceFunction(ParameterInfo parameterInfo, int depth)
            => parameterInfo.Name == "str1"
                ? (new object(), true)
                : (null, false);
        
        [Fact]
        public void Will_not_accept_replace()
            => Assert.Throws<ArgumentException>(()=> Subject.New<TestClass1>(replaceFunction));

    }
}