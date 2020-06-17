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
    
    
    public class When_New_with_two_ctor_with_conflicting_nullable_params : Arrange
    {
        class TestClass
        {
            public TestClass(string s)
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