using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
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
            return joinInfos.SelectMany(c => c.Columns.Select(x => ColumnAttributeExtension.GetColumnNameJoin(x.ColumnAttribute, c, formats)));
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
    /// <typeparam name="TOptions">Options query</typeparam>
    /// <remarks>
    /// Class constructor
    /// </remarks>
    /// <param name="joinInfos">joinInfos</param>
    /// <param name="formats">Formats</param>
    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TOptions>(Queue<JoinInfo> joinInfos, IFormats formats) : QueryBuilderWithCriteria<TJoin, TReturn>(formats),
         IComparisonOperators<TJoin, TReturn, TOptions>,
         IAddJoinCriteria<JoinModel>
         where T1 : class
         where T2 : class
         where TJoin : class
         where TReturn : IQuery<TJoin>
    {
        protected readonly Queue<JoinInfo> _joinInfos = joinInfos ?? new Queue<JoinInfo>();
        protected JoinInfo _joinInfo;

        public IEnumerable<JoinInfo> JoinInfos => _joinInfos;

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
            IEnumerable<string> columns = JoinQueryBuilderWithWhereBase.GetColumns(_joinInfos, Options);
            JoinInfo tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(_joinInfos);
            IEnumerable<string> joinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(_joinInfos, Options);

            string tableName = TableAttributeExtension.GetTableName(tableMain.ClassOptions.Table, Options);
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
            columnsT3 ??= tmp.PropertyOptions;

            _joinInfo = new JoinInfo(columnsT3, tmp, joinType);

            _joinInfos.Enqueue(_joinInfo);
            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

    }
}