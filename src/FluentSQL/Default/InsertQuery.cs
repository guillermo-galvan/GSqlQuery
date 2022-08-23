using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Insert Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class InsertQuery<T> : Query<T> , IExecute<T> where T : class, new()
    {
        public object Entity { get; }

        /// <summary>
        /// Initializes a new instance of the InsertQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public InsertQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions, object entity)
            : base(text, columns, criteria, connectionOptions)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Exec()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            ConnectionOptions.DatabaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment));
            ConnectionOptions.DatabaseManagment.Events.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment.Events));
#pragma warning restore CS8604 // Possible null reference argument.

            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));

            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                var columnAutoIncrementing = Columns.First(x => x.IsAutoIncrementing);
                var propertyOptions = classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == columnAutoIncrementing.Name);
                Text = $"{Text} {ConnectionOptions.DatabaseManagment.ValueAutoIncrementingQuery}";
                object idResult = ConnectionOptions.DatabaseManagment.ExecuteScalar(this, classOptions.PropertyOptions, this.GetParameters(), propertyOptions.PropertyInfo.PropertyType);
                propertyOptions.PropertyInfo.SetValue(Entity, idResult);
            }
            else
            {
                ConnectionOptions.DatabaseManagment.ExecuteNonQuery(this, classOptions.PropertyOptions, this.GetParameters());
            }

            return (T)Entity;
        }
    }
}
