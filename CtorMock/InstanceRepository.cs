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
        
        public T MockOf<T>() where T : class 
            => _mocks[typeof(T)] as T;

        public T CreateMock<T>()
        {
            var instanceFactory = new InstanceFactory(type =>
            {
                if (!_mocks.ContainsKey(type))
                    _mocks.Add(type, _createMock(type));
        
                return _mocks[type];
            } );

            return instanceFactory.New<T>();
        }

        // protected  object CreateMock(Type type)
        // {
        //     if (!_mocks.ContainsKey(type))
        //         _mocks.Add(type, (Mock) Activator.CreateInstance(typeof(Mock<>).MakeGenericType(type)));
        //
        //     return _mocks[type].Object;
        // }
    }
}