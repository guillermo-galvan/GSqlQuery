using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Update query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class UpdateQuery<T> : Query<T>, IExecute<int> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the UpdateQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        { }

        public int Exec()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            ConnectionOptions.DatabaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment));
            ConnectionOptions.DatabaseManagment.Events.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment.Events));
#pragma warning restore CS8604 // Possible null reference argument.
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));

            return ConnectionOptions.DatabaseManagment.ExecuteNonQuery(this, classOptions.PropertyOptions, this.GetParameters());
        }
    }
}
