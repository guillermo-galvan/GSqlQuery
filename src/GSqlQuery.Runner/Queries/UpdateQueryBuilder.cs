using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSqlQuery.Runner.Queries
{
    internal class UpdateQueryBuilder<T, TDbConnection> : GSqlQuery.Queries.UpdateQueryBuilder<T, UpdateQuery<T, TDbConnection>>,
        IQueryBuilder<UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class, new()
    {
        new public ConnectionOptions<TDbConnection> Options { get; }

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, IEnumerable<string> selectMember, object value) :
             base(connectionOptions.Statements, selectMember, value)
        {
            Options = connectionOptions;
        }

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity, IEnumerable<string> selectMember) :
            base(connectionOptions.Statements, entity, selectMember)
        {
            Options = connectionOptions;
        }

        public override UpdateQuery<T, TDbConnection> Build()
        {
            return new UpdateQuery<T, TDbConnection>(CreateQuery(Options.Statements), Columns, _criteria, Options);
        }

        public ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Set<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            AddSet(expression, value);
            return this;
        }

        public ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Set<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            AddSet(_entity, expression);
            return this;
        }

        IWhere<UpdateQuery<T, TDbConnection>> IQueryBuilderWithWhere<UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.Where()
        {
            return Where();
        }
    }
}