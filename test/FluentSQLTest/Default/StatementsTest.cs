using FluentSQL.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Default
{
    public class StatementsTest
    {
        [Fact]
        public void Declarations_cannot_be_null_or_different_than_default()
        {
            Statements statements = new();

            Assert.NotNull(statements.Format);
            Assert.Equal("{0}", statements.Format);

            Assert.NotNull(statements.SelectText);
            Assert.Equal("SELECT {0} FROM {1};", statements.SelectText);

            Assert.NotNull(statements.SelectWhereText);
            Assert.Equal("SELECT {0} FROM {1} WHERE {2};", statements.SelectWhereText);

            Assert.NotNull(statements.InsertText);
            Assert.Equal("INSERT INTO {0} ({1}) VALUES ({2});", statements.InsertText);

            Assert.NotNull(statements.UpdateText);
            Assert.Equal("UPDATE {0} SET {1} WHERE {2};", statements.UpdateText);

            Assert.NotNull(statements.DeleteWhereText);
            Assert.Equal("DELETE FROM {0} WHERE {1};", statements.DeleteWhereText);
        }
    }
}
