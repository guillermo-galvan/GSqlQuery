using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class JoinQueryBuilderWithWhere<T1, T2, TDbConnection> :
        QueryBuilderWithCriteria<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, TDbConnection>,
        IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>>,
        IAddJoinCriteria<JoinModel>
        //, IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, TDbConnection>

        where T1 : class, new()
        where T2 : class, new()
    {

        private readonly Queue<JoinInfo> _joinInfos;
        private readonly JoinInfo _tableNameT2;

        public JoinQueryBuilderWithWhere(string tableName, IEnumerable<PropertyOptions> columns, JoinEnum joinEnum, ConnectionOptions<TDbConnection> connectionOptions,
           IEnumerable<PropertyOptions> columnsT2 = null) : base(connectionOptions)
        {
            _joinInfos = new Queue<JoinInfo>();

            _joinInfos.Enqueue(new JoinInfo
            {
                TableName = tableName,
                Columns = columns,
                IsMain = true,
            });

            var tmp = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _tableNameT2 = new JoinInfo
            {
                TableName = tmp.Table.GetTableName(ConnectionOptions.Statements),
                Columns = columnsT2 ?? tmp.GetPropertyQuery(tmp.PropertyOptions.Select(x => x.PropertyInfo.Name)),
                JoinEnum = joinEnum
            };

            _joinInfos.Enqueue(_tableNameT2);

            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        public void AddColumns(JoinModel joinModel)
        {
            _tableNameT2.Joins.Enqueue(joinModel);
        }

        public override JoinQuery<Join<T1, T2>, TDbConnection> Build()
        {
            var query = JoinQueryBuilderWithWhere<T1,T2>.CreateQuery(_andOr != null, Statements, _joinInfos, _andOr != null ? GetCriteria() : "");
            return new JoinQuery<Join<T1, T2>, TDbConnection>(query, Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions);
        }

        public override IWhere<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>> Where()
        {
            _andOr = new AndOrJoin<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>>(this);
            return (IWhere<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>>)_andOr;
        }
    }

    //internal class JoinQueryBuilderWithWhere<T1, T2, T3, TDbConnection> : QueryBuilderWithCriteria<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, TDbConnection>,
    //    IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>,TDbConnection>,
    //    IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>>,
    //    IAddJoinCriteria<JoinModel>
    //    where T1 : class, new()
    //    where T2 : class, new()
    //    where T3 : class, new()
    //{
    //    private readonly Queue<JoinInfo> _joinInfos;
    //    private readonly JoinInfo _tableNameT3;

    //    public JoinQueryBuilderWithWhere(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions)
    //    {
    //    }

    //    public void AddColumns(JoinModel joinModel)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override JoinQuery<Join<T1, T2, T3>, TDbConnection> Build()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override IWhere<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>> Where()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
