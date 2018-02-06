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

// Class being tested
    public class MyApp
    {
        private readonly IMyDatabase _myDatabase;
        private readonly IAppSetting _appSetting;

        public MyApp(IMyDatabase myDatabase, IAppSetting appSetting)
        {
            _myDatabase = myDatabase;
            _appSetting = appSetting;
        }

        public IEnumerable<Product> RunQuery(string query)
        {
            var dbObjects = _myDatabase.Execute(_appSetting.ConnectionString, query);
            return dbObjects.Select(s => new Product {Id = s.Id});
        }
    }

//Test use case
    namespace Given_MyApp
    {
        public abstract class Arrange : MockBase<MyApp>
        {
            protected Arrange()
            {
                Mocker.MockOf<IAppSetting>().Setup(s => s.ConnectionString)
                    .Returns(ConnectionString);

                Mocker.MockOf<IMyDatabase>().Setup(s => s.Execute(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(SqlResult);
            }

            protected virtual string ConnectionString { get; } = "";
            protected virtual IEnumerable<DbObject> SqlResult { get; } = null;
        }


        public class When_RunQuery : Arrange
        {
            private readonly IEnumerable<Product> _result;
            protected override string ConnectionString => "myConnectionString";

            protected override IEnumerable<DbObject> SqlResult => new[]
            {
                new DbObject(),
                new DbObject()
            };

            public When_RunQuery()
                => _result = Subject.RunQuery("select * from products");

            [Fact]
            public void Should_Use_connectionString_from_appSettings()
                => Mocker.MockOf<IMyDatabase>().Verify(v => v.Execute("myConnectionString", It.IsAny<string>()));

            [Fact]
            public void Should_execute_query()
                => Assert.Equal(2, _result.Count());

        }
    }
```






