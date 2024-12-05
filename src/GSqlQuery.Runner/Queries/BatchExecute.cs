using GSqlQuery.Runner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class BatchExecute<TDbConnection> : IExecute<int, TDbConnection>
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;
        private readonly List<IQuery> _queries;
        private readonly List<IDataParameter> _parameters;
        private readonly StringBuilder _queryBuilder;
        private readonly List<KeyValuePair<string, PropertyOptions>> _columns;
        private uint _paramId = 0;

        public ConnectionOptions<TDbConnection> DatabaseManagement => _connectionOptions;

        IDatabaseManagement<TDbConnection> IExecute<int, TDbConnection>.DatabaseManagement => _connectionOptions.DatabaseManagement;

        internal BatchExecute(ConnectionOptions<TDbConnection> connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _queries = new List<IQuery>();
            _parameters = new List<IDataParameter>();
            _queryBuilder = new StringBuilder();
            _columns = [];
        }

        public BatchExecute<TDbConnection> Add<T>(Func<ConnectionOptions<TDbConnection>, IQuery<T, ConnectionOptions<TDbConnection>>> expression)
            where T : class
        {
            IQuery query = expression.Invoke(_connectionOptions);
            IEnumerable<IDataParameter> parameters = GeneralExtension.GetParameters<T, TDbConnection>(query, _connectionOptions.DatabaseManagement);

            string paramName = string.Empty;
            Dictionary<string, string> replacements = [];


            if (parameters.Any())
            {
                foreach (IDataParameter item in parameters)
                {
                    paramName = item.ParameterName + _paramId++;
                    replacements.Add(item.ParameterName, paramName);
                    item.ParameterName = paramName;
                    _parameters.Add(item);
                }

                _queryBuilder.Append(ReemplazarTextoConRegex(query.Text, replacements));
            }
            else
            {
                _queryBuilder.Append(query.Text);
            }

            _columns.AddRange(query.Columns);

            _queries.Add(query);
            return this;
        }

        public static string ReemplazarTextoConRegex(string input, Dictionary<string, string> replacements)
        {
            string pattern = "(" + string.Join("|", replacements.Keys) + ")";
            return Regex.Replace(input, pattern, match => replacements[match.Value]);
        }

        public int Execute()
        {
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQuery(query, _parameters);
        }

        public int Execute(TDbConnection connection)
        {
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQuery(connection, query, _parameters);
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQueryAsync(query, _parameters, cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection connection, CancellationToken cancellationToken = default)
        {
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQueryAsync(connection, query, _parameters, cancellationToken);
        }
    }
}