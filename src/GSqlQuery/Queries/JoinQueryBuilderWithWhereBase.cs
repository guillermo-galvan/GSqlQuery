using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal static class JoinQueryBuilderWithWhereBase
    {
        internal static JoinInfo GetTableMain(IEnumerable<JoinInfo> joinInfos)
        {
            return joinInfos.First(x => x.IsMain);
        }

        internal static IEnumerable<string> GetColumns(IEnumerable<JoinInfo> joinInfos, IFormats statements)
        {
            return joinInfos.SelectMany(c => c.Columns.Select(x => x.ColumnAttribute.GetColumnNameJoin(c, statements)));
        }

        internal static IEnumerable<string> CreateJoinQuery(IEnumerable<JoinInfo> joinInfos, IFormats statements)
        {
            Queue<string> joinQuerys = new Queue<string>();

            foreach (var item in joinInfos.Where(x => !x.IsMain))
            {
                var a = string.Join(" ", item.Joins.Select(x => CreateJoinQueryPart(statements, x)));

                joinQuerys.Enqueue($"{GetJoinQuery(item.JoinEnum)} {string.Format(ConstFormat.JOIN, item.ClassOptions.Table.GetTableName(statements), a)}");
            }

            return joinQuerys;
        }

        internal static string GetJoinQuery(JoinType joinEnum)
        {
            switch (joinEnum)
            {
                case JoinType.Inner:
                    return "INNER";
                case JoinType.Left:
                    return "LEFT";
                case JoinType.Right:
                    return "RIGHT";
                case JoinType.None:
                default:
                    return "";
            }
        }

        internal static string CreateJoinQueryPart(IFormats statements, JoinModel joinModel)
        {
            string partRight = joinModel.JoinModel1.Column.GetColumnName(joinModel.JoinModel1.Table.GetTableName(statements), statements, QueryType.Join);
            string partLeft = joinModel.JoinModel2.Column.GetColumnName(joinModel.JoinModel2.Table.GetTableName(statements), statements, QueryType.Join);

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

        public JoinQueryBuilderWithWhereBase(Queue<JoinInfo> joinInfos, IFormats statements) : base(statements)
        {
            _joinInfos = joinInfos ?? new Queue<JoinInfo>();
        }

        public override IWhere<TJoin, TReturn> Where()
        {
            _andOr = new AndOrJoin<T1, T2, TJoin, TReturn, TOptions>((IQueryBuilderWithWhere<TReturn, TOptions>)this);
            return (IWhere<TJoin, TReturn>)_andOr;
        }

        public void AddColumns(JoinModel joinModel)
        {
            _joinInfo.Joins.Enqueue(joinModel);
        }

        internal string CreateQuery(IFormats statements)
        {
            var columns = JoinQueryBuilderWithWhereBase.GetColumns(_joinInfos, statements);
            var tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(_joinInfos);
            IEnumerable<string> JoinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(_joinInfos, statements);

            string result;
            string tableName = tableMain.ClassOptions.Table.GetTableName(statements);

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

    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, T3, TJoin, TReturn, TOptions> : JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TOptions>,
        IComparisonOperators<TJoin, TReturn, TOptions>,
        IAddJoinCriteria<JoinModel>
        where T1 : class
        where T2 : class
        where T3 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {
        public override IWhere<TJoin, TReturn> Where()
        {
            _andOr = new AndOrJoin<T1, T2, T3, TJoin, TReturn, TOptions>((IQueryBuilderWithWhere<TReturn, TOptions>)this);
            return (IWhere<TJoin, TReturn>)_andOr;
        }

        public JoinQueryBuilderWithWhereBase(Queue<JoinInfo> joinInfos, JoinType joinEnum, IFormats statements, IEnumerable<PropertyOptions> columnsT3 = null)
            : base(joinInfos, statements)
        {
            var tmp = ClassOptionsFactory.GetClassOptions(typeof(T3));
            _joinInfo = new JoinInfo
            {
                Columns = columnsT3 ?? tmp.GetPropertyQuery(tmp.PropertyOptions.Select(x => x.PropertyInfo.Name)),
                JoinEnum = joinEnum,
                ClassOptions = tmp,
            };

            _joinInfos.Enqueue(_joinInfo);
            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

    }
}