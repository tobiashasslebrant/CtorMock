using System.Collections;
using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_with_parameterless_ctor : Arrange
    {
        class TestClass
        {
            public TestClass()
            {
                
            }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(New<TestClass>());
    }
    
    public class When_New_with_parameter_in_ctor : Arrange
    {
        class TestClass
        {
            public TestClass(object o)
            {
                
            }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(New<TestClass>());
    }
    
    public class When_New_with_string_parameter_in_ctor : Arrange
    {
        class TestClass
        {
            public TestClass(string o)
            {
                
            }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(New<TestClass>());
    }
    
    public class When_New_with_array_parameter_in_ctor : Arrange
    {
        class TestClass
        {
            public TestClass(object[] o)
            {
                
            }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(New<TestClass>());
    }
    public class When_New_with_valuetype_parameter_in_ctor : Arrange
    {
        class TestClass
        {
            public TestClass(int o)
            {
                
            }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(New<TestClass>());
    }
    
    public class When_New_with_interface_parameter_in_ctor : Arrange
    {
        class TestClass
        {
            public TestClass(IComparer comparer)
            { 
            }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(New<TestClass>());
        
    }
}