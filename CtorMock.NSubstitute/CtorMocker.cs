using NSubstitute;

namespace CtorMock.NSubstitute
{
    public class CtorMocker : CtorMockerBase
    {
        readonly Dictionary<Type, object?> _mocks = new();

        public T? MockOf<T>() where T : class
            => (T?)_mocks[typeof(T)];

        public override object? CreateMock(Type type)
        {
            if (!_mocks.ContainsKey(type))
                _mocks.Add(type, Substitute.For(new[] {type}, null));

            return _mocks[type];
        }
    }
}
