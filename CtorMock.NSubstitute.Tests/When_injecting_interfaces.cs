using NSubstitute;
using Xunit;

namespace CtorMock.NSubstitute.Tests
{
    public interface ITest1 { string Name { get; } }
    public interface ITest2 { string Name { get; } }
    public interface ITest3 { string Name { get; } }

    public class Test1 : ITest1
    {
        private readonly ITest2 _test2;
        private readonly ITest3 _test3;

        public Test1(ITest2 test2, ITest3 test3)
        {
            _test2 = test2;
            _test3 = test3;
        }

        public string Name => _test2.Name + _test3.Name;
    }
    
    public class When_injecting_interfaces : MockBase<Test1>
    {
        public When_injecting_interfaces()
        {
            Mocker.MockOf<ITest2>().Name.Returns("test2");
            Mocker.MockOf<ITest3>().Name.Returns("test3");
        }

        [Fact]
        public void Should_have_mocked_the_interfaces() 
            => Assert.Equal("test2test3", Subject.Name);
    }
}
