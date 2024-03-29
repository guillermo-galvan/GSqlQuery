﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using GSqlQuery.Benchmarks.Data;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Benchmarks.Query
{
    public abstract class SelectBenchmark : CreateStaments
    {
        public SelectBenchmark() : base()
        {
        }

        [Benchmark]
        public IWhere<User, SelectQuery<User>> GenerateWhereQuery()
        {
            return User.Select(_formats).Where();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return User.Select(_formats).Build();
        }

        [Benchmark]
        public IQuery GenerateManyColumnsQuery()
        {
            return User.Select(_formats, x => new { x.Name, x.LastName, x.IsActive }).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return User.Select(_formats).Where().Equal(x => x.Id, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQuery()
        {
            return User.Select(_formats).Where().Between(x => x.Id, 1, 2).Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQuery()
        {
            return User.Select(_formats).Where().Like(x => x.Name, "23").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQuery()
        {
            return User.Select(_formats).Where().IsNull(x => x.Name).Build();
        }
    }

    public class Select : SelectBenchmark
    {
        private readonly IEnumerable<int> _ids;
        public Select() : base()
        {
            _ids = Enumerable.Range(0, 1);
        }

        [Benchmark]
        public IQuery GenerateInWhereQuery()
        {
            return User.Select(_formats).Where().In(x => x.Id, _ids).Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQuery()
        {
            return User.Select(_formats).Where()
                       .In(x => x.Id, _ids)
                       .AndEqual(x => x.Name, "nombre")
                       .OrBetween(x => x.Id, 1, 10)
                       .AndIsNull(x => x.LastName)
                       .AndNotLike(x => x.Email, ".gob")
                       .Build();
        }

    }
}