using FluentSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Models
{
    internal class Statements : IStatements
    {
        public string Format => "[{0}]";

        public string SelectText => "SELECT {0} FROM {1};";

        public string SelectWhereText => "SELECT {0} FROM {1} WHERE {2};";

        public string InsertText => "INSERT INTO {0} ({1}) VALUES ({2});";

        public string UpdateText => "UPDATE {0} SET {1} WHERE {2};";

        public string DeleteWhereText => "DELETE FROM {0} WHERE {1};";
    }
}
