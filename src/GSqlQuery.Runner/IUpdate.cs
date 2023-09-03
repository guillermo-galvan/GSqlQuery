using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GSqlQuery.Runner
{
    public interface IUpdate<T> : GSqlQuery.IUpdate<T> where T : class, new()
    {
        ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
           Expression<Func<T, TProperties>> expression);
    }
}