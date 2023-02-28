﻿using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class InsertQueryBuilderTest
    {
        private readonly IStatements _statements;

        public InsertQueryBuilderTest()
        {
            _statements = new Statements();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQueryBuilder<Test3> queryBuilder = new InsertQueryBuilder<Test3>(_statements, new Test3(1, null, DateTime.Now, true));

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Options);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertQueryBuilder<Test1>(null, new Test3(1, null, DateTime.Now, true)));
            Assert.Throws<ArgumentNullException>(() => new InsertQueryBuilder<Test1>(_statements, null));
        }

        [Fact]
        public void Should_return_an_insert_query()
        {
            InsertQueryBuilder<Test1> queryBuilder = new InsertQueryBuilder<Test1>(_statements, new Test1(1, null, DateTime.Now, true));
            IQuery<Test1> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }
    }
}
