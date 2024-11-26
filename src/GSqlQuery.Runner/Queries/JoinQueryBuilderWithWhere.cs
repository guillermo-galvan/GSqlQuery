using GSqlQuery.Cache;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Runner.Queries
{
    internal class JoinQueryBuilderWithWhere<T1, T2, TDbConnection> :
        JoinQueryBuilderWithWhereBase<T1, T2, Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, TDbConnection>,
        IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>>,
        GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>
        where T1 : class
        where T2 : class
    {
        public JoinQueryBuilderWithWhere(JoinType joinType, ConnectionOptions<TDbConnection> connectionOptions, DynamicQuery dynamicQuerySecond) : base(null, connectionOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(classOptions, true);
            _joinInfos.Add(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo(dynamicQuerySecond, classOptions2, joinType);

            _joinInfos.Add(_joinInfo);
        }

        public JoinQueryBuilderWithWhere(JoinType joinType, ConnectionOptions<TDbConnection> connectionOptions) : base(null, connectionOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(classOptions, true);
            _joinInfos.Add(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo(classOptions2, joinType);

            _joinInfos.Add(_joinInfo);
        }

        public JoinQueryBuilderWithWhere(DynamicQuery dynamicQueryMain, JoinType joinType, ConnectionOptions<TDbConnection> connectionOptions, DynamicQuery dynamicQuerySecond) : base(null, connectionOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(dynamicQueryMain, classOptions, true);
            _joinInfos.Add(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo(dynamicQuerySecond, classOptions2, joinType);
            _joinInfos.Add(_joinInfo);
        }

        public JoinQueryBuilderWithWhere(DynamicQuery dynamicQueryMain, JoinType joinType, ConnectionOptions<TDbConnection> connectionOptions) : base(null, connectionOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(dynamicQueryMain, classOptions, true);
            _joinInfos.Add(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo(classOptions2, joinType);
            _joinInfos.Add(_joinInfo);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> Join<TJoin>(JoinType joinEnum)
            where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, joinEnum, QueryOptions);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> Join<TJoin, TProperties>(JoinType joinEnum, Func<TJoin, TProperties> func)
           where TJoin : class
        {
            Type tmp = typeof(TJoin);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(tmp);
            var result = func((TJoin)options.Entity);
            DynamicQuery dynamicQuery = new DynamicQuery(tmp, result.GetType());
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, joinEnum, QueryOptions, dynamicQuery);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Inner);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Left);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Right);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Inner, func);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Left, func);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Right, func);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>) new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Inner, QueryOptions);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Left, QueryOptions);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Right, QueryOptions);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>(Func<TJoin, object> func)
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Inner, func);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>(Func<TJoin, object> func)
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Left, func);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>(Func<TJoin, object> func)
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Right, func);
        }

        IWhere<Join<T1, T2>, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> IQueryBuilderWithWhere<Join<T1, T2>, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.Where()
        {
            return (IWhere<Join<T1, T2>, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)base.Where();
        }

        IWhere<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>> IQueryBuilderWithWhere<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.Where()
        {
            return (IWhere<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>>)base.Where();
        }

        GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>> IBuilder<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>>.Build()
        {
            return Build();
        }

        public override JoinQuery<Join<T1, T2>, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new JoinQuery<Join<T1, T2>, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions, ClassOptionsFactory.GetClassOptions(typeof(T2)).FormatTableName.Table);
        }
    }

    internal class JoinQueryBuilderWithWhere<T1, T2, T3, TDbConnection> :
        JoinQueryBuilderWithWhereBase<T1, T2, T3, Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>>,
         IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, TDbConnection>
        where T1 : class
        where T2 : class
        where T3 : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="joinInfos">Join infos</param>
        /// <param name="joinType">Join Type</param>
        /// <param name="formats">Formats</param>
        /// <param name="columnsT3">Columns third table</param>
        public JoinQueryBuilderWithWhere(List<JoinInfo> joinInfos, JoinType joinType, ConnectionOptions<TDbConnection> connectionOptions) :
            base(joinInfos, joinType, connectionOptions)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="joinInfos">Join infos</param>
        /// <param name="joinType">Join Type</param>
        /// <param name="formats">Formats</param>
        /// <param name="columnsT3">Columns third table</param>
        public JoinQueryBuilderWithWhere(List<JoinInfo> joinInfos, JoinType joinType, ConnectionOptions<TDbConnection> connectionOptions, DynamicQuery dynamicQuery = null) :
            base(joinInfos, joinType, connectionOptions, dynamicQuery)
        { }


        public override JoinQuery<Join<T1, T2, T3>, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new JoinQuery<Join<T1, T2, T3>, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions, ClassOptionsFactory.GetClassOptions(typeof(T2)).FormatTableName.Table, ClassOptionsFactory.GetClassOptions(typeof(T3)).FormatTableName.Table);
        }
    }
}