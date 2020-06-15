using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
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
}