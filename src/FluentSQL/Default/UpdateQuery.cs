﻿using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    /// <summary>
    /// Update query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class UpdateQuery<T> : Query<T>, ISetDatabaseManagement<int> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the UpdateQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        { }

        public IExecute<int, TDbConnection> SetDatabaseManagement<TDbConnection>(IDatabaseManagement<TDbConnection> databaseManagment)
        {
            databaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(databaseManagment));
            return new UpdateExecute<TDbConnection, T>(databaseManagment, this);
        }
    }
}