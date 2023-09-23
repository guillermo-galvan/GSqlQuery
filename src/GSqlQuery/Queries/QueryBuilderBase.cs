using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    public abstract class QueryBuilderBase<T, TReturn> : IBuilder<TReturn>, IQueryBuilder<TReturn, IFormats>
        where T : class where TReturn : IQuery<T>
    {
        private readonly IFormats _statements;
        protected readonly string _tableName;
        protected readonly ClassOptions _classOptions;

        public IEnumerable<PropertyOptions> Columns { get; protected set; }

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IFormats Options => _statements;

        public QueryBuilderBase(IFormats statements)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            Columns = _classOptions.PropertyOptions;
            _tableName = _classOptions.Table.GetTableName(statements);
        }

        public abstract TReturn Build();
    }
}