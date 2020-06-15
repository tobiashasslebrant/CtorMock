using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
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
            => Assert.NotNull(Subject.New<TestClass>());
    }
}