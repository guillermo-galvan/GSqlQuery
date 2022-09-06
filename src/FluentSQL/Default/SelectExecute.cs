using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    internal class SelectExecute<TDbConnection, T> : IExecute<IEnumerable<T>, TDbConnection> where T : class, new()
    {
        private readonly IDatabaseManagement<TDbConnection> _databaseManagment;
        private readonly IEnumerable<PropertyOptions> _propertyOptions;
        private readonly SelectQuery<T> _query;

        public SelectExecute(IDatabaseManagement<TDbConnection> databaseManagment, IEnumerable<PropertyOptions> propertyOptions,SelectQuery<T> query)
        {
            _databaseManagment = databaseManagment ?? throw new ArgumentNullException(nameof(databaseManagment));
            _propertyOptions = propertyOptions ?? throw new ArgumentNullException(nameof(propertyOptions));
            _query = query;
        }

        public IEnumerable<T> Exec()
        {
            return _databaseManagment.ExecuteReader(_query, _propertyOptions, _query.GetParameters(_databaseManagment));
        }

        public IEnumerable<T> Exec(TDbConnection dbConnection)
        {
            return _databaseManagment.ExecuteReader(dbConnection, _query, _propertyOptions, _query.GetParameters(_databaseManagment));
        }
    }
}
