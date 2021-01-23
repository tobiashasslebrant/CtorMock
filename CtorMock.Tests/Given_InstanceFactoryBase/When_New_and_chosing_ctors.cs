using System;
using CtorMock.CtorSelect;
using Xunit;

namespace CtorMock.Tests.Given_InstanceFactoryBase
{
    public class When_New_and_chosing_ctors : Arrange
    {
        class TestClass
        {
            public string ChosenCtor { get; }
            public TestClass2 Inner { get;  }
            
            public TestClass(double s) => ChosenCtor = "depth0:first";
            public TestClass(int i) => ChosenCtor = "depth0:second";
            public TestClass(TestClass2 testClass2)
            {
                ChosenCtor = "depth0:third";
                Inner = testClass2;
            }
        }

        class TestClass2
        {
            public string ChosenCtor { get; }
            public TestClass3 Inner { get; }
            
            public TestClass2(double s) => ChosenCtor = "depth1:first";
            public TestClass2(TestClass3 testClass3)
            {
                ChosenCtor = "depth1:second";
                Inner = testClass3;
            }
        }
        
        
        class TestClass3
        {
            public string ChosenCtor { get; }
            
            public TestClass3(double s) => ChosenCtor = "depth2:first";
            public TestClass3(int i) => ChosenCtor = "depth2:second";
        }
        
       
        [Fact]
        public void Can_chose_first_ctor()
            => Assert.Equal("depth0:first", Subject.New<TestClass>().ChosenCtor);
        
        [Fact]
        public void Can_chose_second_ctor()
            => Assert.Equal("depth0:second", Subject.New<TestClass>(new ctorSel((type) => 1)).ChosenCtor);
        
        [Fact]
        public void Can_chose_different_ctor_for_inner_types()
        {
            var result = Subject.New<TestClass>(new ctorSel((type) =>
            {
                if (type == typeof(TestClass))
                    return 2;

                if (type == typeof(TestClass2))
                    return 1;

                return 0;
            }));
            Assert.Equal("depth0:third", result.ChosenCtor);
            Assert.Equal("depth1:second", result.Inner.ChosenCtor);
            Assert.Equal("depth2:first", result.Inner.Inner.ChosenCtor);
        }


        class ctorSel : ICtorSelecter
        {
            private readonly Func<Type, int> _func;

            public ctorSel(Func<Type,int> func) => _func = func;

            public int Index(Type type)
                => _func(type);

        }
    }
}