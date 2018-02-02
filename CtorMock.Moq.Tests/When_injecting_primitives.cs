using Xunit;

namespace CtorMock.Moq.Tests
{
    public class PrimitiveTest
    {
        public PrimitiveTest(string test1, int test2)
        {
            Test1 = test1;
            Test2 = test2;
        }

        public string Test1 { get; set; }
        public int Test2 { get; set; }
    }

    public class When_injecting_primitives : MockBase<PrimitiveTest>
    {
        [Fact]
        public void Should_have_mocked_string_to_null() 
            => Assert.Null(Subject.Test1);

        [Fact]
        public void Should_have_mocked_int_to_0()
            => Assert.Equal(0, Subject.Test2);
    }
}
