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

        internal static IEnumerable<string> GetColumns(IEnumerable<JoinInfo> joinInfos, IStatements statements)
        {
            return joinInfos.SelectMany(c => c.Columns.Select(x => x.ColumnAttribute.GetColumnNameJoin(c, statements)));
        }

        internal static IEnumerable<string> CreateJoinQuery(IEnumerable<JoinInfo> joinInfos, IStatements statements)
        {
            Queue<string> joinQuerys = new Queue<string>();

            foreach (var item in joinInfos.Where(x => !x.IsMain))
            {
                var a = string.Join(" ", item.Joins.Select(x => CreateJoinQueryPart(statements, x)));

                joinQuerys.Enqueue($"{GetJoinQuery(item.JoinEnum)} {string.Format(statements.Join, item.ClassOptions.Table.GetTableName(statements), a)}");
            }

            return joinQuerys;
        }

        internal static string GetJoinQuery(JoinEnum joinEnum)
        {
            switch (joinEnum)
            {
                case JoinEnum.Inner:
                    return "INNER";
                case JoinEnum.Left:
                    return "LEFT";
                case JoinEnum.Right:
                    return "RIGHT";
                case JoinEnum.None:
                default:
                    return "";
            }
        }

        internal static string CreateJoinQueryPart(IStatements statements, JoinModel joinModel)
        {
            string partRight = joinModel.JoinModel1.Column.GetColumnName(joinModel.JoinModel1.Table.GetTableName(statements), statements);
            string partLeft = joinModel.JoinModel2.Column.GetColumnName(joinModel.JoinModel2.Table.GetTableName(statements), statements);

            string joinCriteria = string.Empty;

            switch (joinModel.JoinCriteria)
            {
                case JoinCriteriaEnum.Equal:
                    joinCriteria = "=";
                    break;
                case JoinCriteriaEnum.NotEqual:
                    joinCriteria = "<>";
                    break;
                case JoinCriteriaEnum.GreaterThan:
                    joinCriteria = ">";
                    break;
                case JoinCriteriaEnum.LessThan:
                    joinCriteria = "<";
                    break;
                case JoinCriteriaEnum.GreaterThanOrEqual:
                    joinCriteria = ">=";
                    break;
                case JoinCriteriaEnum.LessThanOrEqual:
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
         where T1 : class, new()
         where T2 : class, new()
         where TJoin : class, new()
         where TReturn : IQuery<TJoin>
    {
        protected readonly Queue<JoinInfo> _joinInfos;
        protected JoinInfo _joinInfo;

        public IEnumerable<JoinInfo> JoinInfos => _joinInfos;

        public JoinQueryBuilderWithWhereBase(Queue<JoinInfo> joinInfos, IStatements statements) : base(statements)
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

        internal string CreateQuery(IStatements statements)
        {
            var columns = JoinQueryBuilderWithWhereBase.GetColumns(_joinInfos, statements);
            var tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(_joinInfos);
            IEnumerable<string> JoinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(_joinInfos, statements);

            string result;
            string tableName = tableMain.ClassOptions.Table.GetTableName(statements);

            if (_andOr == null)
            {
                result = string.Format(statements.JoinSelect, string.Join(",", columns), tableName, string.Join(" ", JoinQuerys));
            }
            else
            {
                result = string.Format(statements.JoinSelectWhere, string.Join(",", columns), tableName, string.Join(" ", JoinQuerys), GetCriteria());
            }

            return result;
        }
    }

    internal abstract class JoinQueryBuilderWithWhereBase<T1, T2, T3, TJoin, TReturn, TOptions> : JoinQueryBuilderWithWhereBase<T1, T2, TJoin, TReturn, TOptions>,
        IComparisonOperators<TJoin, TReturn, TOptions>,
        IAddJoinCriteria<JoinModel>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where TJoin : class, new()
        where TReturn : IQuery<TJoin>
    {
        public override IWhere<TJoin, TReturn> Where()
        {
            _andOr = new AndOrJoin<T1, T2, T3, TJoin, TReturn, TOptions>((IQueryBuilderWithWhere<TReturn, TOptions>)this);
            return (IWhere<TJoin, TReturn>)_andOr;
        }

        public JoinQueryBuilderWithWhereBase(Queue<JoinInfo> joinInfos, JoinEnum joinEnum, IStatements statements, IEnumerable<PropertyOptions> columnsT3 = null)
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