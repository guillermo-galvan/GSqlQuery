﻿using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    public abstract class QueryBuilderBase<T, TReturn> : IBuilder<TReturn> where T : class, new() where TReturn : IQuery
    {
        private readonly IStatements _statements;
        protected QueryType _queryType;
        protected readonly string _tableName;

        public IEnumerable<PropertyOptions> Columns { get; protected set; }

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IStatements Statements => _statements;

        public QueryBuilderBase(IStatements statements, QueryType queryType)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions;
            _tableName = ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements);
            _queryType = queryType;
        }

        protected void ChangeQueryType()
        {
            switch (_queryType)
            {
                case QueryType.Select:
                    _queryType = QueryType.SelectWhere;
                    break;
                case QueryType.Update:
                    _queryType = QueryType.UpdateWhere;
                    break;
                case QueryType.Delete:
                    _queryType = QueryType.DeleteWhere;
                    break;
                default:
                    _queryType = QueryType.Custom;
                    break;
            }
        }

        protected abstract string GenerateQuery();

        public abstract TReturn Build();
    }
}