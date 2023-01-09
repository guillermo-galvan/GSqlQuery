﻿using System;

namespace GSqlQuery.Runner
{
    public abstract class QueryBuilderBase<T, TReturn, TDbConnection> : QueryBuilderBase<T, TReturn>, IQueryBuilder<T, TReturn>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {
        public ConnectionOptions<TDbConnection> ConnectionOptions { get; }

        public QueryBuilderBase(ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions != null ? connectionOptions.Statements : null)
        {
            ConnectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }
    }
}
