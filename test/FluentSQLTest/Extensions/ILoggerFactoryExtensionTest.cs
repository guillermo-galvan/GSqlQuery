using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentSQL.Extensions;

namespace FluentSQLTest.Extensions
{
    public class ILoggerFactoryExtensionTest
    {
        [Fact]
        public void Should_save_the_message()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            try
            {
                factory?.LogWarning<ILoggerFactoryExtensionTest>("Test", "Test 1");
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
