using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
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
    }
}