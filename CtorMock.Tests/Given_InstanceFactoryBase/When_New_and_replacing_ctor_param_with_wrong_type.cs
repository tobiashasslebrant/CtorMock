using System;
using System.Reflection;
using CtorMock.ParamReplacing;
using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
    public class When_New_and_replacing_ctor_param_with_wrong_type : Arrange
    {
        class TestClass1
        {
            public TestClass1(string str1){}
        }
        
        class TestReplaceFunction : IParamReplace
        {
            public bool CanReplace(ParameterInfo parameterInfo, Type parent) 
                => parameterInfo.Name == "str1";

            public object GetReplacement(ParameterInfo parameterInfo, Type parent) 
                => new object();
            //
            // public (object replaceWith, bool isReplaced) Replace(ParameterInfo parameterInfo, Type parent)
            //     => parameterInfo.Name == "str1"
            //         ? (new object(), true)
            //         : (null, false);
        }

        [Fact]
        public void Will_not_accept_replace()
            => Assert.Throws<ArgumentException>(()=> Subject.New<TestClass1>(new TestReplaceFunction()));

    }
}