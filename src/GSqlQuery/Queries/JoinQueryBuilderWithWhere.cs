using System.Collections.Generic;
using GSqlQuery.Extensions;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Reflection;

namespace GSqlQuery.Queries
{
    internal class JoinQueryBuilderWithWhere<T1, T2> : QueryBuilderWithCriteria<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>>, 
        IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>>,
        IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>>,
        IAddJoinCriteria<JoinModel>
        where T1 : class, new()
        where T2 : class, new()
    {
        private readonly Queue<JoinInfo> _joinInfos;
        private readonly JoinInfo _tableNameT2;

        public JoinQueryBuilderWithWhere(string tableName,IEnumerable<PropertyOptions> columns, JoinEnum joinEnum, IStatements statements,
            IEnumerable<PropertyOptions> columnsT2 = null) : base(statements) 
        {
            _joinInfos = new Queue<JoinInfo>();

            _joinInfos.Enqueue(new JoinInfo 
            {
                TableName = tableName,
                Columns= columns,
                IsMain = true,
            });

            var tmp = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _tableNameT2 = new JoinInfo
            {
                TableName = tmp.Table.GetTableName(statements),
                Columns = columnsT2 ?? tmp.GetPropertyQuery(tmp.PropertyOptions.Select(x => x.PropertyInfo.Name)),
                JoinEnum = joinEnum
            };

            _joinInfos.Enqueue(_tableNameT2);

            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        internal static string CreateQuery(bool isWhere, IStatements statements, IEnumerable<JoinInfo> joinInfos,
            string criterias)
        {
            var columns = joinInfos.Select(x => new { x.TableName ,x.Columns }).SelectMany(c => c.Columns.Select(x => x.ColumnAttribute.GetColumnName(c.TableName, statements)));
            var tableMain = joinInfos.First(x => x.IsMain);
            Queue<string> JoinQuerys = new Queue<string>();

            foreach (var item in joinInfos.Where(x => !x.IsMain))
            {
                var a = string.Join(" ", item.Joins.Select(x => CreateJoinQueryPart(statements, x)));

                JoinQuerys.Enqueue($"{GetJoinQuery(item.JoinEnum)} {string.Format(statements.Join, item.TableName, a)}");
            }

            string result = string.Empty;

            if (!isWhere)
            {
                result = string.Format(statements.JoinSelect, string.Join(",", columns), tableMain.TableName, string.Join(" ", JoinQuerys));
            }
            else
            {
                result = string.Format(statements.JoinSelectWhere, string.Join(",", columns), tableMain.TableName, string.Join(" ", JoinQuerys), criterias);
            }

            return result;
        }

        private static string GetJoinQuery(JoinEnum joinEnum)
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

        private static string CreateJoinQueryPart(IStatements statements, JoinModel joinModel)
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

            return string.IsNullOrWhiteSpace(joinModel.LogicalOperator) ? $"{partRight} {joinCriteria} { partLeft}" : 
                $"{joinModel.LogicalOperator} {partRight} {joinCriteria} {partLeft}";
        }

        public IComparisonOperators<JoinThreeTables<T1, T2, TJoin>, JoinQuery<JoinThreeTables<T1, T2, TJoin>>> InnerJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinEnum.Inner, Statements);
        }

        public IComparisonOperators<JoinThreeTables<T1, T2, TJoin>, JoinQuery<JoinThreeTables<T1, T2, TJoin>>> LeftJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinEnum.Left, Statements);
        }

        public IComparisonOperators<JoinThreeTables<T1, T2, TJoin>, JoinQuery<JoinThreeTables<T1, T2, TJoin>>> RightJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinEnum.Right, Statements);
        }

        public void AddColumns(JoinModel joinModel)
        {
            _tableNameT2.Joins.Enqueue(joinModel);
        }

        public override IWhere<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> Where()
        {
            _andOr = new AndOrJoin<T1,T2>(this);
            return (IWhere<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>>)_andOr;
        }

        public override JoinQuery<JoinTwoTables<T1, T2>> Build()
        {
            var query = CreateQuery(_andOr != null, Statements, _joinInfos, _andOr != null ? GetCriteria() : "");
            return new JoinQuery<JoinTwoTables<T1, T2>>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        private IComparisonOperators<JoinThreeTables<T1, T2, TJoin>, JoinQuery<JoinThreeTables<T1, T2, TJoin>>> Join<TJoin, TProperties>(JoinEnum joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class, new()
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var selectMember = options.MemberInfo.Select(x => x.Name);
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, joinEnum, Statements, ClassOptionsFactory.GetClassOptions(typeof(TJoin)).GetPropertyQuery(selectMember));
        }

        public IComparisonOperators<JoinThreeTables<T1, T2, TJoin>, JoinQuery<JoinThreeTables<T1, T2, TJoin>>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression) 
            where TJoin : class, new()
        {
           return Join(JoinEnum.Inner, expression);
        }

        public IComparisonOperators<JoinThreeTables<T1, T2, TJoin>, JoinQuery<JoinThreeTables<T1, T2, TJoin>>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinEnum.Left, expression);
        }

        public IComparisonOperators<JoinThreeTables<T1, T2, TJoin>, JoinQuery<JoinThreeTables<T1, T2, TJoin>>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinEnum.Right, expression);
        }
    }

    internal class JoinQueryBuilderWithWhere<T1, T2, T3> : QueryBuilderWithCriteria<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>>,
        IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>>,
        IComparisonOperators<JoinThreeTables<T1, T2,T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>>, 
        IAddJoinCriteria<JoinModel>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
    {
        private readonly Queue<JoinInfo> _joinInfos;
        private readonly JoinInfo _tableNameT3;

        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinEnum joinEnum, IStatements statements, IEnumerable<PropertyOptions> columnsT3 = null) : base(statements)
        {
            _joinInfos = joinInfos;
            var tmp = ClassOptionsFactory.GetClassOptions(typeof(T3));
            _tableNameT3 = new JoinInfo
            {
                TableName = tmp.Table.GetTableName(statements),
                Columns = columnsT3 ?? tmp.GetPropertyQuery(tmp.PropertyOptions.Select(x => x.PropertyInfo.Name)),
                JoinEnum = joinEnum
            };

            _joinInfos.Enqueue(_tableNameT3);
            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        public void AddColumns(JoinModel joinModel)
        {
            _tableNameT3.Joins.Enqueue(joinModel);
        }

        public override JoinQuery<JoinThreeTables<T1, T2, T3>> Build()
        {
            var query = JoinQueryBuilderWithWhere<T1, T2>.CreateQuery(_andOr != null, Statements, _joinInfos, _andOr != null ? GetCriteria() : "");
            return new JoinQuery<JoinThreeTables<T1, T2,T3>>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        public override IWhere<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> Where()
        {
            _andOr = new AndOrJoin<T1, T2, T3>(this);
            return (IWhere<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>>)_andOr;
        }
    }

}