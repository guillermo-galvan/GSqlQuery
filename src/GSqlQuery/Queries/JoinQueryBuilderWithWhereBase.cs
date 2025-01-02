using GSqlQuery.Cache;
using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal class ColumnDetailJoin(string partquery, string key, PropertyOptions propertyOptions)
    {
        public string PartQuery { get; private set; } = partquery;

        public string Key { get; private set; } = key;

        public PropertyOptions PropertyOptions { get; private set; } = propertyOptions;
    }

    internal class QueryPartColumns(List<ColumnDetailJoin> columns, List<string> joinQueries)
    {
        public List<ColumnDetailJoin> Columns { get; private set; } = columns;

        public List<string> JoinQuerys { get; private set; } = joinQueries;
    }

    /// <summary>
    /// Join Query Builder With Where Base
    /// </summary>
    internal static class JoinQueryBuilderWithWhereBase
    {
        /// <summary>
        /// Get Columns
        /// </summary>
        /// <param name="joinInfos">Join info</param>
        /// <param name="formats">Formats</param>
        /// <returns></returns>
        internal static QueryPartColumns GetColumns(IEnumerable<JoinInfo> joinInfos, IFormats formats)
        {
            List<ColumnDetailJoin> columnDetailJoins = [];
            List<string> joinQueries = [];

            foreach (JoinInfo joinInfo in joinInfos)
            {
                PropertyOptionsCollection columns = null;
                if (joinInfo.DynamicQuery != null)
                {
                    ClassOptionsTupla<PropertyOptionsCollection> options = ExpressionExtension.GeTQueryOptionsAndMembersByFunc(joinInfo.DynamicQuery);
                    ExpressionExtension.ValidateClassOptionsTupla(QueryType.Join, options);
                    columns = options.Columns;
                }
                else
                {
                    columns = joinInfo.ClassOptions.PropertyOptions;
                }

                foreach (KeyValuePair<string, PropertyOptions> item in columns)
                {
                    string alias = formats.Format.Replace("{0}", $"{joinInfo.ClassOptions.Type.Name}_{item.Value.ColumnAttribute.Name}");
                    string tableName = joinInfo.ClassOptions.FormatTableName.GetTableName(formats);
                    string columnName = item.Value.FormatColumnName.GetColumnName(formats, QueryType.Join);
                    string queryPart = "{0} as {1}".Replace("{0}", columnName).Replace("{1}", alias);
                    columnDetailJoins.Add(new ColumnDetailJoin(queryPart, alias, item.Value));
                }

                if (!joinInfo.IsMain)
                {
                    string a = string.Join(" ", joinInfo.Joins.Select(x => CreateJoinQueryPart(formats, x)));

                    joinQueries.Add($"{GetJoinQuery(joinInfo.JoinEnum)} {string.Format(ConstFormat.JOIN, joinInfo.ClassOptions.FormatTableName.GetTableName(formats), a)}");
                }
            }

            return new QueryPartColumns(columnDetailJoins, joinQueries);
        }

        /// <summary>
        /// Get Join Query 
        /// </summary>
        /// <param name="joinType">Join type</param>
        /// <returns></returns>
        internal static string GetJoinQuery(JoinType joinType)
        {
            return joinType switch
            {
                JoinType.Inner => "INNER",
                JoinType.Left => "LEFT",
                JoinType.Right => "RIGHT",
                _ => "",
            };
        }

        /// <summary>
        /// Create Join Query part
        /// </summary>
        /// <param name="formats">formats</param>
        /// <param name="joinModel"> Join model</param>
        /// <returns></returns>
        internal static string CreateJoinQueryPart(IFormats formats, JoinModel joinModel)
        {
            var joinModel1 = ExpressionExtension.GetJoinColumn(joinModel.JoinModel1.Expression);
            var joinModel2 = ExpressionExtension.GetJoinColumn(joinModel.JoinModel2.Expression);

            string left = joinModel1.Value.FormatColumnName.GetColumnName(formats, QueryType.Join);
            string right = joinModel2.Value.FormatColumnName.GetColumnName(formats, QueryType.Join);

            string joinCriteria = string.Empty;

            switch (joinModel.JoinCriteria)
            {
                case JoinCriteriaType.Equal:
                    joinCriteria = "=";
                    break;
                case JoinCriteriaType.NotEqual:
                    joinCriteria = "<>";
                    break;
                case JoinCriteriaType.GreaterThan:
                    joinCriteria = ">";
                    break;
                case JoinCriteriaType.LessThan:
                    joinCriteria = "<";
                    break;
                case JoinCriteriaType.GreaterThanOrEqual:
                    joinCriteria = ">=";
                    break;
                case JoinCriteriaType.LessThanOrEqual:
                    joinCriteria = "<=";
                    break;
            }

            return string.IsNullOrWhiteSpace(joinModel.LogicalOperator) ? $"{left} {joinCriteria} {right}" :
                $"{joinModel.LogicalOperator} {left} {joinCriteria} {right}";
        }
    }

    /// <summary>
    /// Join Query Builder With Where Base
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    /// <typeparam name="TJoin">Type for third table</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options query</typeparam>
    /// <remarks>
    /// Class constructor
    /// </remarks>
    /// <param name="joinInfos">joinInfos</param>
    /// <param name="formats">Formats</param>
    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TQueryOptions>(List<JoinInfo> joinInfos, TQueryOptions options) :
        QueryBuilderWithCriteria<TJoin, TReturn, TQueryOptions>(options),
         IComparisonOperators<TJoin, TReturn, TQueryOptions>,
         IAddJoinCriteria<JoinModel>,
         IQueryBuilderWithWhere<TReturn, TQueryOptions>
         where T1 : class
         where T2 : class
         where TJoin : class
         where TReturn : JoinQuery<TJoin, TQueryOptions>
         where TQueryOptions : QueryOptions
    {
        protected readonly List<JoinInfo> _joinInfos = joinInfos ?? new List<JoinInfo>();
        protected JoinInfo _joinInfo;

        public IEnumerable<JoinInfo> JoinInfos => _joinInfos;

        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="TJoin"/>, <typeparamref name="TReturn"/>&gt;</returns>
        public override IWhere<TJoin, TReturn, TQueryOptions> Where()
        {
            _andOr = new AndOrJoin<T1, T2, TJoin, TReturn, TQueryOptions>(this, QueryOptions);
            return (IWhere<TJoin, TReturn, TQueryOptions>)_andOr;
        }

        /// <summary>
        /// Add column
        /// </summary>
        /// <param name="joinModel">Join Model</param>
        public void AddColumns(JoinModel joinModel)
        {
            _joinInfo.Joins.Add(joinModel);
        }

        /// <summary>
        /// <returns>Query text</returns>
        /// Create Query
        /// </summary>
        internal string CreateQueryText(out PropertyOptionsCollection keyValuePairs)
        {
            QueryPartColumns queryPartColumns = JoinQueryBuilderWithWhereBase.GetColumns(_joinInfos, QueryOptions.Formats);
            keyValuePairs = new PropertyOptionsCollection(queryPartColumns.Columns.Select(x => new KeyValuePair<string, PropertyOptions>(x.Key, x.PropertyOptions)));
            JoinInfo tableMain = _joinInfos.First(x => x.IsMain);

            string tableName = tableMain.ClassOptions.FormatTableName.GetTableName(QueryOptions.Formats);
            string resultColumns = string.Join(",", queryPartColumns.Columns.Select(x => x.PartQuery));
            string resultJoinQuerys = string.Join(" ", queryPartColumns.JoinQuerys);

            if (_andOr == null)
            {
                return ConstFormat.JOINSELECT.Replace("{0}", resultColumns).Replace("{1}", tableName).Replace("{2}", resultJoinQuerys);
            }
            else
            {
                string criteria = GetCriteria();
                return ConstFormat.JOINSELECTWHERE.Replace("{0}", resultColumns).Replace("{1}", tableName).Replace("{2}", resultJoinQuerys).Replace("{3}", criteria);
            }
        }

        public override TReturn Build()
        {
            return CacheQueryBuilderExtension.CreateJoinQuery<TJoin, TReturn, TQueryOptions>(QueryOptions, _joinInfos, _andOr, CreateQuery, GetQuery);
        }

        public abstract TReturn GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions);

        public TReturn CreateQuery()
        {
            string text = CreateQueryText(out PropertyOptionsCollection columns);
            TReturn result = GetQuery(text, columns, _criteria, QueryOptions);
            return result;
        }
    }

    /// <summary>
    /// Join Query Builder With Where Base
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    /// <typeparam name="T3">Type for third table</typeparam>
    /// <typeparam name="TJoin">Table type</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options Query</typeparam>

    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, T3, TJoin, TReturn, TQueryOptions> : JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TQueryOptions>,
        IComparisonOperators<TJoin, TReturn, TQueryOptions>,
        IAddJoinCriteria<JoinModel>
        where T1 : class
        where T2 : class
        where T3 : class
        where TJoin : class
        where TReturn : JoinQuery<TJoin, TQueryOptions>
         where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="TJoin"/>, <typeparamref name="TReturn"/>&gt;</returns>
        public override IWhere<TJoin, TReturn, TQueryOptions> Where()
        {
            _andOr = new AndOrJoin<T1, T2, T3, TJoin, TReturn, TQueryOptions>(this, QueryOptions);
            return (IWhere<TJoin, TReturn, TQueryOptions>)_andOr;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="joinInfos">List<JoinInfo></param>
        /// <param name="joinType">Join Type</param>
        /// <param name="formats">formats</param>
        /// <param name="columnsT3">Columns third table</param>

        public JoinQueryBuilderWithWhereBase(List<JoinInfo> joinInfos, JoinType joinType, TQueryOptions options)
            : base(joinInfos, options)
        {
            ClassOptions tmp = ClassOptionsFactory.GetClassOptions(typeof(T3));
            _joinInfo = new JoinInfo(tmp, joinType);
            _joinInfos.Add(_joinInfo);
        }

        public JoinQueryBuilderWithWhereBase(List<JoinInfo> joinInfos, JoinType joinType, TQueryOptions options, DynamicQuery dynamicQuery = null)
            : base(joinInfos, options)
        {
            ClassOptions tmp = ClassOptionsFactory.GetClassOptions(typeof(T3));
            _joinInfo = new JoinInfo(dynamicQuery, tmp, joinType);
            _joinInfos.Add(_joinInfo);
        }

    }
}