using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
	public class When_replacing_object_with_object : Arrange
	{
		private Test1 _result;

		class Test1
		{
			public InnerTest InnerTest { get; set; } 

			public Test1(InnerTest innerTest)
			{
				InnerTest = innerTest;
			}
		}

		class InnerTest
		{
			public string Name { get; set; }
		}
        
		public When_replacing_object_with_object()
		{
			_result = Subject.New<Test1>(("innerTest", new InnerTest
			{
				Name = "a name"
			}));
		}

		[Fact]
		public void Must_have_replaced_object()
			=> Assert.Equal("a name", _result.InnerTest.Name);
	}
}