using Xunit;

namespace CtorMock.Moq.Tests
{
    public class When_injecting_interfaces : MockBase<With_ctor_interfaces>
    {
        public When_injecting_interfaces()
        {
            Mocker.MockOf<IInjected1>().Setup(s => s.Name).Returns("test2");
            Mocker.MockOf<IInjected2>().Setup(s => s.Name).Returns("test3");
        }

        [Fact]
        public void Should_have_mocked_the_interfaces() 
            => Assert.Equal("test2test3", Subject.Name);
        
        
    }
}
