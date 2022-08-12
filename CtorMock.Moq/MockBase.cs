using System;
using System.Dynamic;

namespace CtorMock.Moq
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

        [Obsolete("will work for now, but will be removed")]
        protected MockBase(ExpandoObject overrideMock)
        {
            Mocker = new CtorMocker();
            Subject = Mocker.New<T>(overrideMock);
        }

        protected MockBase(params (string paramName, object replacedWith)[] paramReplaces)
        {
            Mocker = new CtorMocker();
            Subject = Mocker.New<T>(paramReplaces);

        }
        protected MockBase(int ctorIndex, params (string paramName, object replacedWith)[] paramReplaces)
        {
            Mocker = new CtorMocker();
            Subject = Mocker.New<T>(ctorIndex, paramReplaces);
        }
    }
}