﻿using GSqlQuery.Extensions;
using GSqlQuery.Runner;
using GSqlQuery.Runner.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{ 
    public class SelectQuery<T, TDbConnection> : Query<T, TDbConnection, IEnumerable<T>>, IQuery<T, TDbConnection, IEnumerable<T>>,
        IExecute<IEnumerable<T>, TDbConnection> where T : class, new()
    {
        internal SelectQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        {
        }

        public override IEnumerable<T> Execute()
        {
            return DatabaseManagment.ExecuteReader<T>(this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagment.ExecuteReader<T>(dbConnection, this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override Task<IEnumerable<T>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return DatabaseManagment.ExecuteReaderAsync<T>(this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
        }

        public override Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagment.ExecuteReaderAsync<T>(dbConnection, this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
        }
    }
}