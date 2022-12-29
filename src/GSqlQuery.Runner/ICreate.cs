﻿using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;

namespace GSqlQuery.Runner
{
    public interface ICreate<T> : GSqlQuery.ICreate<T> where T : class, new()
    {
        IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions);
    }
}