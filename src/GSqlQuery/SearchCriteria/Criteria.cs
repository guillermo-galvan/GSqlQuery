using System;
using System.Collections.Generic;

namespace GSqlQuery.SearchCriteria
{
    public abstract class Criteria : ISearchCriteria
    {
        protected static ulong _idParam = 0;

        /// <summary>
        /// Get Column
        /// </summary>
        public ColumnAttribute Column { get; }

        /// <summary>
        /// Get Table
        /// </summary>
        public TableAttribute Table { get; }

        /// <summary>
        /// Get logical operator
        /// </summary>
        public string LogicalOperator { get; }

        /// <summary>
        /// Initializes a new instance of the Criteria class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="logicalOperator">Logical Operator </param>
        /// <exception cref="ArgumentNullException"></exception>
        public Criteria(TableAttribute table, ColumnAttribute columnAttribute, string logicalOperator)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            Column = columnAttribute ?? throw new ArgumentNullException(nameof(columnAttribute));
            LogicalOperator = logicalOperator;
            if (_idParam > ulong.MaxValue - 2100)
            {
                _idParam = 0;
            }
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public abstract CriteriaDetail GetCriteria(IStatements statements, IEnumerable<PropertyOptions> propertyOptions);
    }
}