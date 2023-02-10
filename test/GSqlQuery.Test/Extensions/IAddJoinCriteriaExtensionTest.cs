using GSqlQuery.Test.Models;
using System.Linq.Expressions;
using System;
using Xunit;
using GSqlQuery.Queries;
using GSqlQuery.Extensions;

namespace GSqlQuery.Test.Extensions
{
    public class IAddJoinCriteriaExtensionTest
    {
        private readonly IStatements _stantements;

        public IAddJoinCriteriaExtensionTest()
        {
            _stantements= new Statements();
        }

        [Fact]
        public void AddColumnJoin_two_tables()
        {
            var addJoinCriteria = (IAddJoinCriteria<JoinModel>)Test3.Select(_stantements).InnerJoin<Test6>();

            Expression<Func<JoinTwoTables<Test3, Test6>, int>> expression1 = x => x.Table1.Ids;
            Expression<Func<JoinTwoTables<Test3, Test6>, int>> expression2 = x => x.Table2.Ids;

            addJoinCriteria.AddColumnJoin(null,expression1, JoinCriteriaEnum.Equal,expression2);
        }

        [Fact]
        public void AddColumnJoin_three_tables()
        {
            var addJoinCriteria = (IAddJoinCriteria<JoinModel>)Test3.Select(_stantements).InnerJoin<Test6>();

            Expression<Func<JoinThreeTables<Test3, Test6, Test1>, int>> expression1 = x => x.Table1.Ids;
            Expression<Func<JoinThreeTables<Test3, Test6, Test1>, int>> expression2 = x => x.Table3.Id;

            addJoinCriteria.AddColumnJoin(null, expression1, JoinCriteriaEnum.Equal, expression2);
        }
    }
}
