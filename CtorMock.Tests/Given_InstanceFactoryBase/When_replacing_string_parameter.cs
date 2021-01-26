using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
	public class When_replacing_string_parameter : Arrange
	{
		private Test1 _result;

		class Test1
		{
			public Test1(string str)
			{
				Str = str;
			}

			public string Str { get; set; }
		}

		public When_replacing_string_parameter()
		{
			_result = Subject.New<Test1>(("str", "a string"));
			
		}

		[Fact]
		public void Must_have_replaced_string()
			=> Assert.Equal("a string", _result.Str);
	}
}