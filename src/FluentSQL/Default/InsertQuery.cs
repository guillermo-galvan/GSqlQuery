using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Data.Common;

namespace FluentSQL.Default
{
    /// <summary>
    /// Insert Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class InsertQuery<T> : Query<T,T> , IExecute<T> where T : class, new()
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

        private void InsertAutoIncrementing(DbConnection? connection = null)
        {
            var classOptions = GetClassOptions();
            var columnAutoIncrementing = Columns.First(x => x.IsAutoIncrementing);
            var propertyOptions = classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == columnAutoIncrementing.Name);            

            object idResult;
            if (connection == null)
            {
                idResult = ConnectionOptions.DatabaseManagment.ExecuteScalar(this, this.GetParameters(), 
                    propertyOptions.PropertyInfo.PropertyType);
            }
            else
            {
                idResult = ConnectionOptions.DatabaseManagment.ExecuteScalar(connection, this, this.GetParameters(), 
                    propertyOptions.PropertyInfo.PropertyType);
            }
            
            propertyOptions.PropertyInfo.SetValue(Entity, idResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T Exec()
        {
            ValidateDbManagment();

            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing();
            }
            else
            {
                ConnectionOptions.DatabaseManagment.ExecuteNonQuery(this, this.GetParameters());
            }

            return (T)Entity;
        }

        public override T Exec(DbConnection connection)
        {
            ValidateDbManagment();

            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing(connection);
            }
            else
            {
                ConnectionOptions.DatabaseManagment.ExecuteNonQuery(connection,this, this.GetParameters());
            }

            return (T)Entity;
        }
    }
}
