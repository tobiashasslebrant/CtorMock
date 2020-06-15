using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_with_recursive_parameter_in_ctor : Arrange
    {
        class TestClass1
        {
            public TestClass1(TestClass2 testClass2) => Inner = testClass2;
            public TestClass2 Inner { get; }
        }
        class TestClass2
        {
            public TestClass2(TestClass3 testClass3) => Inner = testClass3;
            public TestClass3 Inner { get; }

        }
        class TestClass3
        {
        }

        [Fact]
        public void Can_create_nested_objects()
            => Assert.NotNull(Subject.New<TestClass1>().Inner.Inner);
        
    }
}