# CtorMock
Used for automocking constructor arguments

# Implementations

There are currently two implementations, one for Moq and one for FakeItEasy

# Instructions

When writing a test inherit from class MockBask<>, and use the class being tested as the generic.

Mockbase will create an instance of the class being tested and add it to property "Subject".
When it creates the instance it will automock all constructor arguments and keep the mocks in memory, the mocks can be accessed by using the method MockOf<>.

Example with Moq and xUnit:
```
  namespace Given_MyApp
    {
        public class When_RunQuery : MockBase<MyApp>
        {
            public When_RunQuery()
                => Mocker.MockOf<IAppSetting>().Setup(s => s.ConnectionString).Returns("myConnectionString");

            [Act]
            public void Should_Execute_query()
                => Mocker.MockOf<IMyDatabase>().Verify(v => v.Execute("myConnectionString", It.IsAny<string>()));

        }
    }
```






