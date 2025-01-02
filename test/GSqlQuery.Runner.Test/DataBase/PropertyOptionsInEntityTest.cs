using GSqlQuery.Runner.Test.Models;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.DataBase
{
    public class PropertyOptionsInEntityTest
    {
        [Fact]
        public void PropertyOptionsInEntity()
        {
            DataReaderPropertyDetail propertyOptionsInEntity = 
                new DataReaderPropertyDetail(ClassOptionsFactory.GetClassOptions(typeof(Test1)).PropertyOptions.Values.FirstOrDefault(), 0);

            Assert.NotNull(propertyOptionsInEntity.Property);
            Assert.NotNull(propertyOptionsInEntity.Ordinal);
            Assert.NotNull(propertyOptionsInEntity.Property .DefaultValue);
        }
    }
}
