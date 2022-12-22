using System;
using System.Linq.Expressions;

namespace GSqlQuery.Runner
{
    public interface IUpdate<T> : GSqlQuery.IUpdate<T> where T : class, new()
    {
        ISet<T, UpdateQuery<T, TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions,
            Expression<Func<T, TProperties>> expression);
    }
}
