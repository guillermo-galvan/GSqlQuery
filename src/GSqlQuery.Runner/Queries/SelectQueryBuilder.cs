using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GSqlQuery.Runner.Test, PublicKey=0024000004800000940000000602000000240000525341310004000001000100913cebd9950f6fcb7fb913297422ef8f3cbdec249d3bbba88346b2045500eeda9546b5fd977bc95be5efb2ca6a8f15a2907dc1bab80d177d2e43b77db77befe6ce26b647e89871a9fede8174dc504ac3322cf5952141cf5fbbdf789fc074bcced5cdc939120d2f67ac483495a97d4df9d3a5fe13f76e40840ee0d70b2dda4b9c")]
namespace GSqlQuery.Runner.Queries
{
    internal class SelectQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, SelectQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, SelectQuery<T, TDbConnection>, TDbConnection>,        
        IBuilder<SelectQuery<T, TDbConnection>>,
        IJoinQueryBuilder<T, SelectQuery<T,TDbConnection>, TDbConnection>
        where T : class, new()
    {
        public SelectQueryBuilder(IEnumerable<string> selectMember, ConnectionOptions<TDbConnection> connectionOptions) :
            base(connectionOptions)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember);
        }

        public override SelectQuery<T, TDbConnection> Build()
        {
            var query = SelectQueryBuilder<T>.CreateQuery(_andOr != null, Statements, Columns, _tableName, _andOr != null ? GetCriteria() : "");
            return new SelectQuery<T, TDbConnection>(query, Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions);
        }

        public override IWhere<T, SelectQuery<T, TDbConnection>> Where()
        {
            _andOr = new AndOrBase<T, SelectQuery<T, TDbConnection>>(this);
            return (IWhere<T, SelectQuery<T, TDbConnection>>)_andOr;
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> 
            Join<TJoin, TProperties>(JoinEnum joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class, new()
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var selectMember = options.MemberInfo.Select(x => x.Name);
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));

            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(_tableName, Columns, joinEnum, ConnectionOptions,
                ClassOptionsFactory.GetClassOptions(typeof(TJoin)).GetPropertyQuery(selectMember));
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> InnerJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T,TJoin,TDbConnection>(_tableName, Columns, JoinEnum.Inner,ConnectionOptions);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression) 
            where TJoin : class, new()
        {
            return Join(JoinEnum.Inner, expression);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> LeftJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(_tableName, Columns, JoinEnum.Left, ConnectionOptions);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinEnum.Left, expression);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> RightJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(_tableName, Columns, JoinEnum.Right, ConnectionOptions);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinEnum.Right, expression);
        }
    }
}
