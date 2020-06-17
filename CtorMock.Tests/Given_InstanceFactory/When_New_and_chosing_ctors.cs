using Xunit;

namespace CtorMock.Tests.Given_InstanceFactory
{
    public class When_New_and_chosing_ctors : Arrange
    {
        class TestClass
        {
            public string ChosenCtor { get; }
            public TestClass2 Inner { get; set; }
            
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
            public TestClass3 Inner { get; set; }
            
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
            
            public TestClass3(double s) => ChosenCtor = "depth3:first";
            public TestClass3(int i) => ChosenCtor = "depth3:second";
        }
        
       
        [Fact]
        public void Can_chose_first_ctor()
            => Assert.Equal("depth0:first", Subject.New<TestClass>().ChosenCtor);
        
        [Fact]
        public void Can_chose_second_ctor()
            => Assert.Equal("depth0:second", Subject.New<TestClass>((type,depth) => 1).ChosenCtor);
        
        [Fact]
        public void Can_chose_different_ctor_for_inner_types()
        {
            var result = Subject.New<TestClass>((type, depth) =>
            {
                if (type == typeof(TestClass) && depth == 0)
                    return 2;

                if (type == typeof(TestClass2) && depth == 1)
                    return 1;

                return 0;
            });
            Assert.Equal("depth0:third", result.ChosenCtor);
            Assert.Equal("depth1:second", result.Inner.ChosenCtor);
            Assert.Equal("depth3:first", result.Inner.Inner.ChosenCtor);

            
        }
    }
}