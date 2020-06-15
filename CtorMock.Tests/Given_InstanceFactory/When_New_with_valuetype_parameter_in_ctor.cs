using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
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
            => Assert.NotNull(Subject.New<TestClass>());
    }
}