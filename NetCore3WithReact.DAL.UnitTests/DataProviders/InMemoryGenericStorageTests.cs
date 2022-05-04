using NetCore3WithReact.DAL.DataProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NetCore3WithReact.DAL.UnitTests.DataProviders
{
    public class InMemoryGenericStorageTests
    {
        private readonly InMemoryGenericStorage<TestClass> _sut;

        public InMemoryGenericStorageTests()
        {
            _sut = new InMemoryGenericStorage<TestClass>();
            _sut.Clear();
        }

        [Fact]
        public void Set_WhenValueIsAddedInSeveralThreads_ValueShouldNotBeDuplicated()
        {
            var tasks = new List<Task>();
            const string key = "some_key";
            const string value = "some_value";

            for (int i = 0; i < 10; i++)
            {
                var index = i;
                var task = Task.Factory.StartNew(() => _sut.Set(key, new TestClass($"{value}_{index}")));
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            var result = _sut.Get(key);
            Assert.NotNull(result);
            Assert.Single(_sut.GetAllItems());
        }

        private class TestClass
        {
            public TestClass()
            {
            }

            public TestClass(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }

    }
}
