    using System;

    namespace CtorMock.Tests.Given_InstanceFactory
    {
        public abstract class Arrange
        {
            public Arrange()
            {
                Subject = new InstanceFactory(type =>
                {
                    if(type.Name == nameof(ITestInterface))
                        return new TestClassWithInterface();
                    
                    return Convert.ChangeType(null, type);
                });
            }
            protected interface ITestInterface{}
            protected class TestClassWithInterface : ITestInterface{}

            public InstanceFactory Subject { get; } 
        }
    }