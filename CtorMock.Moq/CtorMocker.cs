using System;
using System.Collections.Generic;
using Moq;

namespace CtorMock.Moq
{
    public class CtorMocker : CtorMockerBase
    {
        readonly Dictionary<Type, Mock> _mocks = new Dictionary<Type, Mock>();

        public Mock<T> MockOf<T>() where T : class 
            => _mocks[typeof(T)] as Mock<T>;

        protected override object CreateMock(Type type)
        {
            if (!_mocks.ContainsKey(type))
                _mocks.Add(type, (Mock)Activator.CreateInstance(typeof(Mock<>).MakeGenericType(type)));

            return _mocks[type].Object;
        }
    }
}
