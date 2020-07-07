using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
    public class When_New_with_replace_many : Arrange
    {
        class TestClass
        {
            public string S { get; }
            public int I { get; }

            public TestClass(string s, int i)
            {
                S = s;
                I = i;
            }
            
        }
        
        [Fact]
        public void Can_replace_string()
            => Assert.Equal("kalle", Subject.New<TestClass>(("s", "kalle")).S);
        
        [Fact]
        public void Can_replace_int()
            => Assert.Equal(99, Subject.New<TestClass>(("i", 99)).I);
        
        [Fact]
        public void Can_replace_many()
        {
            var subject =  Subject.New<TestClass>(("i", 99), ("s", "kalle"));
            Assert.Equal(99, subject.I);
            Assert.Equal("kalle", subject.S);

        }
    }
}