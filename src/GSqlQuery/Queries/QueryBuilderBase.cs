using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    public abstract class QueryBuilderBase<T, TReturn> : IBuilder<TReturn> where T : class, new() where TReturn : IQuery
    {
        private readonly IStatements _statements;
        protected readonly string _tableName;

        public IEnumerable<PropertyOptions> Columns { get; protected set; }

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IStatements Statements => _statements;

        public QueryBuilderBase(IStatements statements)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions;
            _tableName = ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements);
        }

        public abstract TReturn Build();
    }
}
