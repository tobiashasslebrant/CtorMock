using System.Dynamic;
using Xunit;

namespace CtorMock.Moq.Tests
{
    public interface ITestOverride { string Name { get; set; }}

    public class TestOverride : ITestOverride { public string Name { get; set; } }

    public class CtorOverrideTest
    {
        public string Test1 { get; }
        public int Test2 { get; }
        public ITestOverride Test3 { get; }
        public string Test4 { get; }

        public CtorOverrideTest(string test1, int test2, ITestOverride test3, string test4)
        {
            Test1 = test1;
            Test2 = test2;
            Test3 = test3;
            Test4 = test4;
        }
    }

    public class When_injecting_override_ctor_param
    {
        private readonly CtorOverrideTest _result;

        public When_injecting_override_ctor_param()
        {
            var mocker = new CtorMocker();
            dynamic expand = new ExpandoObject();
            expand.test1 = "kalle";
            expand.test2 = 3;
            expand.test3 = new TestOverride{Name = "TestOverride Name"};

           _result =  mocker.New<CtorOverrideTest>(expand);
        }


        [Fact]
        public void Should_have_overridden_ctor_param_Test1()
            => Assert.Equal("kalle", _result.Test1);

        [Fact]
        public void Should_have_overridden_ctor_param_Test2()
            => Assert.Equal(3, _result.Test2);

        [Fact]
        public void Should_have_overridden_ctor_param_Test3()
            => Assert.Equal("TestOverride Name", _result.Test3.Name);

        [Fact]
        public void Should_have_ignored_ctor_param_Test4()
            => Assert.Null(_result.Test4);
    }


}