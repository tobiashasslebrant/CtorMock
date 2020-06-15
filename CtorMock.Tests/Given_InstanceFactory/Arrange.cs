    using System;
using System.Reflection;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public abstract class Arrange
    {
        protected class MockedObject
        {
            
        }
        
        protected T New<T>(Func<ParameterInfo, (object,bool)> func = null)
        {
            var fact = new InstanceFactory(type => Convert.ChangeType(null, type));
            return fact.New<T>(func);
        }
    }
}