using Xunit;

namespace CtorMock.Moq.Tests
{
	public class When_injecting_override_ctor_param
    {
        private readonly With_ctor_interfaces_and_primitives _result;

        public When_injecting_override_ctor_param()
        {
            var mocker = new CtorMocker();
            
            _result = mocker.New<With_ctor_interfaces_and_primitives>(
                 ("test1", "kalle"),
                 ("test2", 3),
                 ("implementation", new With_Nothing {Name = "TestOverride Name"}));
        }
		
        [Fact]
        public void Should_have_overridden_ctor_param_Test1()
            => Assert.Equal("kalle", _result.Test1);

        [Fact]
        public void Should_have_overridden_ctor_param_Test2()
            => Assert.Equal(3, _result.Test2);

        [Fact]
        public void Should_have_overridden_ctor_param_Test3()
            => Assert.Equal("TestOverride Name", _result.Implementation.Name);

        [Fact]
        public void Should_have_ignored_ctor_param_Test4()
            => Assert.Null(_result.Test4);
    }
}