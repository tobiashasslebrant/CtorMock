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
            => Assert.NotNull(Subject.New<TestClass>());
    }
}