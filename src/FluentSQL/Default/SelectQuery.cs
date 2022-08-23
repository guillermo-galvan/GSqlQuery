using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    /// <summary>
    /// Select query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class SelectQuery<T> : Query<T>, IExecute<IEnumerable<T>> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the SelectQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Exec()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            ConnectionOptions.DatabaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment));
            ConnectionOptions.DatabaseManagment.Events.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment.Events));
#pragma warning restore CS8604 // Possible null reference argument.

            return ConnectionOptions.DatabaseManagment.ExecuteReader(this, ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions, this.GetParameters());
        }
    }
}
