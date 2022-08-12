using System;
using System.Collections.Generic;
using Moq;

namespace CtorMock.Moq
{
    public class CtorMocker : CtorMockerBase
    {
        readonly Dictionary<Type, Mock> _mocks = new();

        public Mock<T> MockOf<T>() where T : class
        {
	        if (!_mocks.ContainsKey(typeof(T)))
		        CreateMock(typeof(T));
			
	        return (Mock<T>)_mocks[typeof(T)];
	    }

        public T? CreateMock<T>() 
        {
            if(!typeof(T).IsInterface)
                throw new Exception("When creating mock, it must be an interface");
            return (T?) CreateMock(typeof(T));
        }

        public override object? CreateMock(Type type)
        {
            if (!_mocks.ContainsKey(type))
            {
                var instance =  Activator.CreateInstance(typeof(Mock<>).MakeGenericType(type));
                _mocks.Add(type, (Mock)instance);
            }

            return _mocks[type].Object;
        }
    }
}
