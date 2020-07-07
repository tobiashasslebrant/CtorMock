using System;
using System.Collections.Generic;
using Moq;

namespace CtorMock.Tests.Given_InstanceFactoryBase
    {
        public class MoqFactory : CtorMockerBase
        {
            public override object CreateMock(Type type) 
                => ((Mock) Activator.CreateInstance(typeof(Mock<>).MakeGenericType(type)))?.Object;
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