using System;
using System.Linq.Expressions;
using Moq;
using Moq.Language.Flow;

namespace CtorMock.Moq
{
    public class BuilderMocker<T> where T : class
    {
        readonly Mock<T> _mock = new Mock<T>();

        public T Object => _mock.Object;

        public BuilderMocker<T> Fake(Expression<Func<T, T>> func)
        {
            _mock.Setup(func).Returns(_mock.Object);
            return this;
        }

        public ISetup<T, TResult> Fake<TResult>(Expression<Func<T, TResult>> func)
            => _mock.Setup(func);
    }
}