    using System;

    namespace CtorMock.Tests.Given_InstanceFactory
    {
        public abstract class Arrange
        {
            public InstanceFactory Subject 
                => new InstanceFactory(type => Convert.ChangeType(null, type));
        }
    }