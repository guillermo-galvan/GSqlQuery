using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Data.Common;

namespace FluentSQL.Default
{
    /// <summary>
    /// Delete query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class DeleteQuery<T> : Query<T,int>, IExecute<int> where T : class, new()
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
        public override int Exec()
        {
            return ConnectionOptions.DatabaseManagment.ExecuteNonQuery(this, GetClassOptions().PropertyOptions,this.GetParameters());
        }

        public override int Exec(DbConnection connection)
        {
            return ConnectionOptions.DatabaseManagment.ExecuteNonQuery(connection,this, GetClassOptions().PropertyOptions, this.GetParameters());
        }
    }
}
