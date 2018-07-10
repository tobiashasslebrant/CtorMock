namespace CtorMock.Moq.Tests
{
	public interface IImplementation { string Name { get; } }
	public interface IInjected1 { string Name { get; } }
	public interface IInjected2 { string Name { get; } }

	public class With_ctor_interfaces : IImplementation
	{
		private readonly IInjected1 _injected1;
		private readonly IInjected2 _injected2;

		public With_ctor_interfaces(IInjected1 injected1, IInjected2 injected2)
		{
			_injected1 = injected1;
			_injected2 = injected2;
		}

		public string Name => _injected1.Name + _injected2.Name;
	}

	public class With_Nothing : IImplementation
	{
		public string Name { get; set; }
	}

	public class With_ctor_interfaces_and_primitives
	{
		public string Test1 { get; }
		public int Test2 { get; }
		public IImplementation Implementation { get; }
		public string Test4 { get; }

		public With_ctor_interfaces_and_primitives(string test1, int test2, IImplementation implementation, string test4)
		{
			Test1 = test1;
			Test2 = test2;
			Implementation = implementation;
			Test4 = test4;
		}
	}

}