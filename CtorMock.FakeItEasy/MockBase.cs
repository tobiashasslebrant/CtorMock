using System.Dynamic;

namespace CtorMock.FakeItEasy
{
    public abstract class MockBase<T> where T : class
    {
        protected CtorMocker Mocker { get; }
        protected T Subject { get; }

        protected MockBase()
        {
            Mocker = new CtorMocker();
            Subject = Mocker.New<T>();
        }

        protected MockBase(ExpandoObject overrideMock)
        {
            Mocker = new CtorMocker();
            Subject = Mocker.New<T>(overrideMock);
        }
    }
}