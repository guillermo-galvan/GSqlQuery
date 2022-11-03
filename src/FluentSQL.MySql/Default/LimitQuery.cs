using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.MySql.Default
{
    public class LimitQuery<T> : Query<T> where T : class, new()
    {
        public LimitQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        {
        }
    }

    public class LimitQuery<T, TDbConnection> : Query<T, TDbConnection, IEnumerable<T>>, IQuery<T, TDbConnection, IEnumerable<T>>,
        IExecute<IEnumerable<T>, TDbConnection> where T : class, new()
    {
        public LimitQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(text, columns, criteria, connectionOptions)
        {
        }

        public override IEnumerable<T> Execute()
        {
            return DatabaseManagment.ExecuteReader<T>(this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection));
            }
            return DatabaseManagment.ExecuteReader<T>(dbConnection, this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }
    }
}
