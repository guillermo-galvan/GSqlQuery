using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Reflection;

namespace GSqlQuery.Runner.Queries
{
    internal class UpdateQueryBuilder<T, TDbConnection> : GSqlQuery.Queries.UpdateQueryBuilder<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilder<UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity, ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla) :
             base(connectionOptions, entity, classOptionsTupla)
        { }

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, ClassOptionsTupla<MemberInfo> classOptionsTupla, object value) :
            base(connectionOptions, classOptionsTupla, value)
        { }

        public override UpdateQuery<T, TDbConnection> Build()
        {
            string text = CreateQuery();
            return new UpdateQuery<T, TDbConnection>(text, Columns, _criteria, QueryOptions);
        }
    }
}