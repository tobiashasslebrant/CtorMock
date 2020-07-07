using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
    public class When_New_with_two_ctor_with_two_nullable_params : Arrange
    {
        class TestClass
        {
            public TestClass(object o)
            {
                
            }
            public TestClass(object[] o)
            {
                
            }
        }

        [Fact]
        public void Can_create_object()
            => Assert.NotNull(Subject.New<TestClass>());
    }
}