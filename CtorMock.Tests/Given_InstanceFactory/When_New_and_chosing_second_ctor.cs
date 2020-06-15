using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_and_chosing_second_ctor : Arrange
    {
        class TestClass
        {
            public string ChosenCtor { get; }
            
            public TestClass(string s) => ChosenCtor = "first";
            public TestClass(string s, int i) => ChosenCtor = "second";
        }

        [Fact]
        public void Can_chose_first_ctor()
            => Assert.Equal("first", Subject.New<TestClass>(0).ChosenCtor);
        [Fact]
        public void Can_chose_second_ctor()
            => Assert.Equal("second", Subject.New<TestClass>(1).ChosenCtor);
        
    }
}