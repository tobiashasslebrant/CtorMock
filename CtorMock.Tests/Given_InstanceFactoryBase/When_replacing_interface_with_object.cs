using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
	public class When_replacing_interface_with_object : Arrange
	{
		private Test1 _result;

		class Test1
		{
			public IInnerTest InnerTest { get; }

			public Test1(IInnerTest innerTest)
			{
				InnerTest = innerTest;
			}
		}

		public interface IInnerTest
		{ 
			string Name { get; set; }

		}
		class InnerTest : IInnerTest
		{
			public string Name { get; set; }
		}
        
		public When_replacing_interface_with_object()
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
	
	public class When_replacing_generic_interface_with_object : Arrange
	{
		private Test1 _result;

		class Test1
		{
			public IInnerTest<string> InnerTest { get; }

			public Test1(IInnerTest<string> innerTest)
			{
				InnerTest = innerTest;
			}
		}

		public interface IInnerTest<T>
		{ 
			T Name { get; set; }

		}
		class InnerTest : IInnerTest<string>
		{
			public string Name { get; set; }
		}
        
		public When_replacing_generic_interface_with_object()
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