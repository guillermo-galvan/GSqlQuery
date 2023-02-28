using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery.Runner.Queries
{
    internal class UpdateQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, UpdateQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhereRunner<T, UpdateQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderRunner<T, UpdateQuery<T, TDbConnection>, TDbConnection>, IBuilder<UpdateQuery<T, TDbConnection>>,
        ISet<T, UpdateQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IDictionary<ColumnAttribute, object> _columnValues;
        protected readonly object _entity;

        public IDictionary<ColumnAttribute, object> ColumnValues => _columnValues;

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, IEnumerable<string> selectMember, object value) :
            base(connectionOptions)
        {
            _columnValues = new Dictionary<ColumnAttribute, object>();
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            foreach (ColumnAttribute item in ClassOptionsFactory.GetClassOptions(typeof(T)).GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity, IEnumerable<string> selectMember) :
            base(connectionOptions)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columnValues = new Dictionary<ColumnAttribute, object>();
            foreach (var item in from prop in ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions
                                 join sel in selectMember on prop.PropertyInfo.Name equals sel
                                 select new { prop.ColumnAttribute, prop.PropertyInfo })
            {
                _columnValues.Add(item.ColumnAttribute, item.PropertyInfo.GetValue(entity));
            };
        }

        public override UpdateQuery<T, TDbConnection> Build()
        {
            var query = GSqlQuery.Queries.UpdateQueryBuilder<T>.CreateQuery(_columnValues, _andOr != null, Statements, Columns, _tableName, _andOr != null ? GetCriteria() : string.Empty, ref _criteria);
            return new UpdateQuery<T, TDbConnection>(query, Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions);
        }

        public override IWhere<T, UpdateQuery<T, TDbConnection>> Where()
        {
            _andOr = new AndOrBase<T,UpdateQuery<T,TDbConnection>>(this);
            return (IWhere<T, UpdateQuery<T, TDbConnection>>)_andOr;
        }

        public ISet<T, UpdateQuery<T, TDbConnection>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression, TProperties value)
        {
            GSqlQuery.Queries.UpdateQueryBuilder<T>.AddSet(_columnValues, expression, value);
            return this;
        }

        public ISet<T, UpdateQuery<T, TDbConnection>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression)
        {
            GSqlQuery.Queries.UpdateQueryBuilder<T>.AddSet(_entity, _columnValues, expression);
            return this;
        }
    }
}
