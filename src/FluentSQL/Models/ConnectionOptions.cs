using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Models
{
    public class ConnectionOptions
    {
        public IStatements Statements { get;}

        public IDatabaseManagment? DatabaseManagment { get;}

        public ConnectionOptions(IStatements statements) :this(statements, null)
        { }

        public ConnectionOptions(IStatements statements, IDatabaseManagment? databaseManagment)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
            DatabaseManagment = databaseManagment;
        }
    }
}
