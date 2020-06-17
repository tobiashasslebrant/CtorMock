using Xunit;

namespace CtorMock.Tests.Given_InstanceRepository
{
    public class Arrange
    {
        public Arrange()
        {
            
        }

        [Fact] 
        public void test()
        {
            var instanceRepository = new InstanceRepository(t => null);
            //instanceRepository.
        }
        
        
    }
}