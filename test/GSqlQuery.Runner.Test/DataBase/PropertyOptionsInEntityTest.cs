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
            PropertyOptionsInEntity propertyOptionsInEntity = 
                new PropertyOptionsInEntity(ClassOptionsFactory.GetClassOptions(typeof(Test1)).PropertyOptions.FirstOrDefault(), typeof(Test1), null, 0);

            Assert.NotNull(propertyOptionsInEntity.Property);
            Assert.NotNull(propertyOptionsInEntity.Type);
            Assert.NotNull(propertyOptionsInEntity.Ordinal);
            Assert.Null(propertyOptionsInEntity.ValueDefault);
        }
    }
}
