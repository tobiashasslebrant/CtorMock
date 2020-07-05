    using System;
    using System.Collections.Generic;
    using Moq;

    namespace CtorMock.Tests.Given_InstanceFactory
    {
        public class MoqFactory : InstanceFactory
        {
            readonly Dictionary<Type, Mock> _mocks = new Dictionary<Type, Mock>();

            public Mock<T> GetMockOf<T>() where T : class 
                => _mocks[typeof(T)] as Mock<T>;

            public override object CreateMock(Type type)
            {
                var mock = (Mock) Activator.CreateInstance(typeof(Mock<>).MakeGenericType(type));
                if (_mocks.ContainsKey(type)) 
                    return _mocks[type].Object;
                
                _mocks.Add(type, mock);
                
                var v = new Mock<ITestInterface>();
                
                return _mocks[type].Object;
            }
        }
        
        public abstract class Arrange
        {
            protected Arrange()
            {
                Subject = new MoqFactory();
            }

            public MoqFactory Subject { get; } 
        }
        public interface ITestInterface{}
    }