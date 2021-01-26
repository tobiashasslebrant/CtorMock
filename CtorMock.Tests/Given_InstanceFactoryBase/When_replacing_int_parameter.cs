using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
	public class When_replacing_int_parameter : Arrange
	{
		private Test1 _result;

		class Test1
		{
			public Test1(int myInt)
			{
				Int = myInt;
			}

			public int Int { get; set; }
		}

		public When_replacing_int_parameter()
		{
			_result = Subject.New<Test1>(("myInt", 99));
			
		}

		[Fact]
		public void Must_have_replaced_string()
			=> Assert.Equal(99, _result.Int);
	}
}