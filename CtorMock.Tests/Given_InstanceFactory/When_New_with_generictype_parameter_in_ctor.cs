using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_with_generictype_parameter_in_ctor : Arrange
    {
        class TestClass1
        {
            public TestClass1(TestClass2<int> testClass2)
            {
                Inner = testClass2;
            }
            
            public TestClass2<int> Inner { get; }
        }

        class TestClass2<T>
        {
            public TestClass2(T val)
            {
                Inner = val;
            }
            
            public T Inner { get; }
        }
        

        [Fact]
        public void Can_create_generic_object()
            => Assert.NotNull(Subject.New<TestClass1>().Inner);
        
        [Fact]
        public void Calling_inner_ctor_with_generic_type()
            => Assert.Equal(0,Subject.New<TestClass1>().Inner.Inner);
    }
}