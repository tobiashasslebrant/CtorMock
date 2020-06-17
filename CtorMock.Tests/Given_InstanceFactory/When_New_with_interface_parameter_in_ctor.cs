using System.Collections;
using System.Runtime.CompilerServices;
using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_with_interface_parameter_in_ctor : Arrange
    {
       
        class TestClass
        {
            public TestClass(ITestInterface testInterface)
            {
                Inner = testInterface;
            }

            public ITestInterface Inner { get; set; }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(Subject.New<TestClass>());
        
        [Fact]
        public void Will_have_correct_implementation()
            => Assert.IsAssignableFrom<TestClassWithInterface>(Subject.New<TestClass>().Inner);
        
        [Fact]
        public void Can_get_interfaces_after_creation()
        {
            var subject = Subject.New<TestClass>();
            
            Assert.NotNull(Subject.MockOf<ITestInterface>());
        }
    }
    
    
}