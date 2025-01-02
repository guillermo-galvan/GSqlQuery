using GSqlQuery.Runner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
        private readonly List<CriteriaDetailCollection> _criteriaDetailCollections;
        private readonly StringBuilder _queryBuilder;
        private readonly List<KeyValuePair<string, PropertyOptions>> _columns;
        private uint _paramId = 0;

        public ConnectionOptions<TDbConnection> DatabaseManagement => _connectionOptions;

        IDatabaseManagement<TDbConnection> IExecute<int, TDbConnection>.DatabaseManagement => _connectionOptions.DatabaseManagement;

        internal BatchExecute(ConnectionOptions<TDbConnection> connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _queries = new List<IQuery>();
            _criteriaDetailCollections = [];
            _queryBuilder = new StringBuilder();
            _columns = [];
        }

        public BatchExecute<TDbConnection> Add<T>(Func<ConnectionOptions<TDbConnection>, IQuery<T, ConnectionOptions<TDbConnection>>> expression)
            where T : class
        {
            IQuery query = expression.Invoke(_connectionOptions);

            string paramName = string.Empty;
            Dictionary<string, string> replacements = [];

            if (query.Criteria != null && query.Criteria.Any())
            {
                foreach (CriteriaDetailCollection criteriaDetailCollection in query.Criteria.Where(x => x.Values.Any()))
                {
                    List<ParameterDetail> parameterDetails = [];

                    foreach (ParameterDetail parameterDetail in criteriaDetailCollection.Values)
                    {
                        paramName = parameterDetail.Name + _paramId++;
                        replacements.Add(parameterDetail.Name, paramName);
                        parameterDetails.Add(new ParameterDetail(paramName, parameterDetail.Value));
                    }

                    if (criteriaDetailCollection.SearchCriteria == null)
                    {
                        _criteriaDetailCollections.Add(new CriteriaDetailCollection(criteriaDetailCollection.QueryPart, criteriaDetailCollection.PropertyOptions, [.. parameterDetails]));
                    }
                    else
                    {
                        _criteriaDetailCollections.Add(new CriteriaDetailCollection(criteriaDetailCollection.SearchCriteria, criteriaDetailCollection.QueryPart, criteriaDetailCollection.PropertyOptions, [.. parameterDetails])); 
                    }
                    
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
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), _criteriaDetailCollections);
            return _connectionOptions.DatabaseManagement.ExecuteNonQuery(query);
        }

        public int Execute(TDbConnection connection)
        {
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), _criteriaDetailCollections);
            return _connectionOptions.DatabaseManagement.ExecuteNonQuery(connection, query);
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), _criteriaDetailCollections);
            return _connectionOptions.DatabaseManagement.ExecuteNonQueryAsync(query, cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection connection, CancellationToken cancellationToken = default)
        {
            BatchQuery query = new BatchQuery(_queryBuilder.ToString(), _queries.First().Table, new Cache.PropertyOptionsCollection(_columns), _criteriaDetailCollections);
            return _connectionOptions.DatabaseManagement.ExecuteNonQueryAsync(connection, query, cancellationToken);
        }
    }
}