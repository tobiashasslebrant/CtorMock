using System;
using System.Collections.Generic;

namespace CtorMock
{
    public class InstanceRepository
    {
        readonly Func<Type, object> _createMock;
        readonly Dictionary<Type, object> _mocks = new Dictionary<Type, object>();

        public InstanceRepository(Func<Type, object> createMock) 
            => _createMock = createMock;
        
        public T Get<T>() 
            => (T)_mocks[typeof(T)];

        public T Create<T>()
        {
            var type = typeof(T);
            if (!_mocks.ContainsKey(type))
                _mocks.Add(type, _createMock(type));
    
            return (T)_mocks[type];
        }
    }
}