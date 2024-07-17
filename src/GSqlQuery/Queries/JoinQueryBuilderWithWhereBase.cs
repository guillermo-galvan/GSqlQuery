using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Query Builder With Where Base
    /// </summary>
    internal static class JoinQueryBuilderWithWhereBase
    {
        /// <summary>
        /// Find the main table
        /// </summary>
        /// <param name="joinInfos">Join Infos</param>
        /// <returns>Join Info</returns>
        internal static JoinInfo GetTableMain(IEnumerable<JoinInfo> joinInfos)
        {
            return joinInfos.First(x => x.IsMain);
        }

        /// <summary>
        /// Retrieves table name and alias for join query
        /// </summary>
        /// <param name="column">Contains the property information</param>
        /// <param name="joinInfo">Contains the information for the join query</param>
        /// <param name="formats">Formats for the column.</param>
        /// <returns>Column name for join query</returns>
        internal static string GetColumnNameJoin(ColumnAttribute column, JoinInfo joinInfo, IFormats formats)
        {
            string alias = formats.Format.Replace("{0}", $"{joinInfo.ClassOptions.Type.Name}_{column.Name}");
            string tableName = TableAttributeExtension.GetTableName(joinInfo.ClassOptions.Table, formats);
            string columnName = formats.GetColumnName(tableName, column, QueryType.Join);
            return "{0} as {1}".Replace("{0}", columnName).Replace("{1}", alias);
        }

        /// <summary>
        /// Get Columns
        /// </summary>
        /// <param name="joinInfos">Join info</param>
        /// <param name="formats">Formats</param>
        /// <returns></returns>
        internal static IEnumerable<string> GetColumns(IEnumerable<JoinInfo> joinInfos, IFormats formats)
        {
            return joinInfos.SelectMany(c => c.Columns.Values.Select(x => GetColumnNameJoin(x.ColumnAttribute, c, formats)));
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="joinInfos">Join info</param>
        /// <param name="formats">Formats</param>
        /// <returns></returns>
        internal static IEnumerable<string> CreateJoinQuery(IEnumerable<JoinInfo> joinInfos, IFormats formats)
        {
            Queue<string> joinQuerys = new Queue<string>();
            IEnumerable<JoinInfo> joins = joinInfos.Where(x => !x.IsMain);
            foreach (JoinInfo item in joins)
            {
                string a = string.Join(" ", item.Joins.Select(x => CreateJoinQueryPart(formats, x)));

                joinQuerys.Enqueue($"{GetJoinQuery(item.JoinEnum)} {string.Format(ConstFormat.JOIN, TableAttributeExtension.GetTableName(item.ClassOptions.Table, formats), a)}");
            }

            return joinQuerys;
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
            string partRight = formats.GetColumnName(TableAttributeExtension.GetTableName(joinModel.JoinModel1.Table, formats), joinModel.JoinModel1.Column, QueryType.Join);
            string partLeft = formats.GetColumnName(TableAttributeExtension.GetTableName(joinModel.JoinModel2.Table, formats), joinModel.JoinModel2.Column, QueryType.Join);

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

            return string.IsNullOrWhiteSpace(joinModel.LogicalOperator) ? $"{partRight} {joinCriteria} {partLeft}" :
                $"{joinModel.LogicalOperator} {partRight} {joinCriteria} {partLeft}";
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
    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TQueryOptions>(Queue<JoinInfo> joinInfos, TQueryOptions options) :
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
        protected readonly Queue<JoinInfo> _joinInfos = joinInfos ?? new Queue<JoinInfo>();
        protected JoinInfo _joinInfo;
        protected IEnumerable<KeyValuePair<string, PropertyOptions>> _properties;

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
            _joinInfo.Joins.Enqueue(joinModel);
        }

        /// <summary>
        /// Create Query
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQuery()
        {
            IEnumerable<string> columns = JoinQueryBuilderWithWhereBase.GetColumns(_joinInfos, QueryOptions.Formats);
            JoinInfo tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(_joinInfos);
            IEnumerable<string> joinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(_joinInfos, QueryOptions.Formats);

            string tableName = TableAttributeExtension.GetTableName(tableMain.ClassOptions.Table, QueryOptions.Formats);
            string resultColumns = string.Join(",", columns);
            string resultJoinQuerys = string.Join(" ", joinQuerys);

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
        /// <param name="joinInfos">Queue<JoinInfo></param>
        /// <param name="joinType">Join Type</param>
        /// <param name="formats">formats</param>
        /// <param name="columnsT3">Columns third table</param>

        public JoinQueryBuilderWithWhereBase(Queue<JoinInfo> joinInfos, JoinType joinType, TQueryOptions options, PropertyOptionsCollection columnsT3 = null)
            : base(joinInfos, options)
        {
            ClassOptions tmp = ClassOptionsFactory.GetClassOptions(typeof(T3));
            columnsT3 ??= tmp.PropertyOptions;

            _joinInfo = new JoinInfo(columnsT3, tmp, joinType);

            _joinInfos.Enqueue(_joinInfo);
            Columns = new PropertyOptionsCollection([]);
            _properties = _joinInfos.SelectMany(x => x.Columns);
        }

    }
}