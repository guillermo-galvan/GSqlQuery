using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Delete query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class DeleteQuery<T> : Query<T>, IExecute<int> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the DeleteQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public DeleteQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        { }
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Exec()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            ConnectionOptions.DatabaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment));
            ConnectionOptions.DatabaseManagment.Events.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment.Events));
#pragma warning restore CS8604 // Possible null reference argument.
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));

            return ConnectionOptions.DatabaseManagment.ExecuteNonQuery(this, classOptions.PropertyOptions,this.GetParameters());
        }
    }
}
