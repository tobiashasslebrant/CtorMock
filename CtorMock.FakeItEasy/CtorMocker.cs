using System;
using System.Collections.Generic;
using FakeItEasy.Sdk;

namespace CtorMock.FakeItEasy
{
    public class CtorMocker : CtorMockerBase
    {
        readonly Dictionary<Type, object> _mocks = new Dictionary<Type, object>();

        public T MockOf<T>() where T : class
            => _mocks[typeof(T)] as T;

        protected override object CreateMock(Type type)
        {
            if (_mocks.ContainsKey(type))
                return _mocks[type];

            var mock = Create.Fake(type);
            _mocks.Add(type, mock);
            return mock;
        }
    }
}
