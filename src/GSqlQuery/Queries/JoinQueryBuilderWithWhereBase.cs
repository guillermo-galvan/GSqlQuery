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
        /// Get Columns
        /// </summary>
        /// <param name="joinInfos">Join info</param>
        /// <param name="formats">Formats</param>
        /// <returns></returns>
        internal static IEnumerable<string> GetColumns(IEnumerable<JoinInfo> joinInfos, IFormats formats)
        {
            return joinInfos.SelectMany(c => c.Columns.Select(x => x.ColumnAttribute.GetColumnNameJoin(c, formats)));
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

            foreach (var item in joinInfos.Where(x => !x.IsMain))
            {
                var a = string.Join(" ", item.Joins.Select(x => CreateJoinQueryPart(formats, x)));

                joinQuerys.Enqueue($"{GetJoinQuery(item.JoinEnum)} {string.Format(ConstFormat.JOIN, item.ClassOptions.Table.GetTableName(formats), a)}");
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
            string partRight = joinModel.JoinModel1.Column.GetColumnName(joinModel.JoinModel1.Table.GetTableName(formats), formats, QueryType.Join);
            string partLeft = joinModel.JoinModel2.Column.GetColumnName(joinModel.JoinModel2.Table.GetTableName(formats), formats, QueryType.Join);

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
    /// <typeparam name="TOptions">Options query</typeparam>
    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TOptions> : QueryBuilderWithCriteria<TJoin, TReturn>,
         IComparisonOperators<TJoin, TReturn, TOptions>,
         IAddJoinCriteria<JoinModel>
         where T1 : class
         where T2 : class
         where TJoin : class
         where TReturn : IQuery<TJoin>
    {
        protected readonly Queue<JoinInfo> _joinInfos;
        protected JoinInfo _joinInfo;

        public IEnumerable<JoinInfo> JoinInfos => _joinInfos;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="joinInfos">joinInfos</param>
        /// <param name="formats">Formats</param>
        public JoinQueryBuilderWithWhereBase(Queue<JoinInfo> joinInfos, IFormats formats) : base(formats)
        {
            _joinInfos = joinInfos ?? new Queue<JoinInfo>();
        }

        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="TJoin"/>, <typeparamref name="TReturn"/>&gt;</returns>
        public override IWhere<TJoin, TReturn> Where()
        {
            _andOr = new AndOrJoin<T1, T2, TJoin, TReturn, TOptions>((IQueryBuilderWithWhere<TReturn, TOptions>)this, Options);
            return (IWhere<TJoin, TReturn>)_andOr;
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
            var columns = JoinQueryBuilderWithWhereBase.GetColumns(_joinInfos, Options);
            var tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(_joinInfos);
            IEnumerable<string> JoinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(_joinInfos, Options);

            string result;
            string tableName = tableMain.ClassOptions.Table.GetTableName(Options);

            if (_andOr == null)
            {
                result = string.Format(ConstFormat.JOINSELECT, string.Join(",", columns), tableName, string.Join(" ", JoinQuerys));
            }
            else
            {
                result = string.Format(ConstFormat.JOINSELECTWHERE, string.Join(",", columns), tableName, string.Join(" ", JoinQuerys), GetCriteria());
            }

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
    /// <typeparam name="TOptions">Options Query</typeparam>

    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, T3, TJoin, TReturn, TOptions> : JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TOptions>,
        IComparisonOperators<TJoin, TReturn, TOptions>,
        IAddJoinCriteria<JoinModel>
        where T1 : class
        where T2 : class
        where T3 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {
        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="TJoin"/>, <typeparamref name="TReturn"/>&gt;</returns>
        public override IWhere<TJoin, TReturn> Where()
        {
            _andOr = new AndOrJoin<T1, T2, T3, TJoin, TReturn, TOptions>((IQueryBuilderWithWhere<TReturn, TOptions>)this, Options);
            return (IWhere<TJoin, TReturn>)_andOr;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="joinInfos">Queue<JoinInfo></param>
        /// <param name="joinType">Join Type</param>
        /// <param name="formats">formats</param>
        /// <param name="columnsT3">Columns third table</param>

        public JoinQueryBuilderWithWhereBase(Queue<JoinInfo> joinInfos, JoinType joinType, IFormats formats, IEnumerable<PropertyOptions> columnsT3 = null)
            : base(joinInfos, formats)
        {
            var tmp = ClassOptionsFactory.GetClassOptions(typeof(T3));
            _joinInfo = new JoinInfo
            {
                Columns = columnsT3 ?? tmp.GetPropertyQuery(tmp.PropertyOptions.Select(x => x.PropertyInfo.Name)),
                JoinEnum = joinType,
                ClassOptions = tmp,
            };

            _joinInfos.Enqueue(_joinInfo);
            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

    }
}