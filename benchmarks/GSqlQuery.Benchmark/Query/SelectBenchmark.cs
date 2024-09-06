using BenchmarkDotNet.Attributes;
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
        public IQuery GenerateQuery()
        {
            return User.Select(_queryOptions).Build();
        }

        [Benchmark]
        public IQuery GenerateManyColumnsQuery()
        {
            return User.Select(_queryOptions, x => new { x.Name, x.LastName, x.IsActive }).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return User.Select(_queryOptions).Where().Equal(x => x.Id, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQuery()
        {
            return User.Select(_queryOptions).Where().Between(x => x.Id, 1, 2).Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQuery()
        {
            return User.Select(_queryOptions).Where().Like(x => x.Name, "23").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQuery()
        {
            return User.Select(_queryOptions).Where().IsNull(x => x.Name).Build();
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
            return User.Select(_queryOptions).Where().In(x => x.Id, _ids).Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQuery()
        {
            return User.Select(_queryOptions).Where()
                       .In(x => x.Id, _ids)
                       .AndEqual(x => x.Name, "nombre")
                       .OrBetween(x => x.Id, 1, 10)
                       .AndIsNull(x => x.LastName)
                       .AndNotLike(x => x.Email, ".gob")
                       .Build();
        }

        //[Benchmark]
        //public IQuery GenerateCountOneColumn()
        //{
        //    return User.Select(_queryOptions, x => new { x.Id }).Count().Where().In(x => x.Id, _ids).Build();
        //}

        //[Benchmark]
        //public IQuery GenerateCountAllColumns()
        //{
        //    return User.Select(_queryOptions).Count().Where().In(x => x.Id, _ids).Build();
        //}

        //[Benchmark]
        //public IQuery GenerateOrderBy()
        //{
        //    return User.Select(_queryOptions).OrderBy(x => x.Id, OrderBy.ASC).Build();
        //}

        //[Benchmark]
        //public IQuery GenerateOrderByWithWhere()
        //{
        //    return User.Select(_queryOptions).Where().Equal(x => x.Id, 1).OrderBy(x => x.Id, OrderBy.ASC).Build();
        //}

        //[Benchmark]
        //public IQuery GenerateOrderByMany()
        //{
        //    return User.Select(_queryOptions).OrderBy(x => x.Id, OrderBy.ASC).OrderBy(x => x.Name, OrderBy.DESC).Build();
        //}

        //[Benchmark]
        //public IQuery GenerateOrderBy_new()
        //{
        //    return User.Select(_queryOptions).OrderBy(x => new { x.Id, x.Name }, OrderBy.ASC).Build();
        //}

        //[Benchmark]
        //public IQuery GenerateOrderByWithWhere_new()
        //{
        //    return User.Select(_queryOptions).Where().Equal(x => x.Id, 1).OrderBy(x => x.Id, OrderBy.ASC).Build();
        //}

        //[Benchmark]
        //public IQuery GenerateOrderByMany_new()
        //{
        //    return User.Select(_queryOptions).OrderBy(x => new { x.Id }, OrderBy.ASC).OrderBy(x => new { x.Name }, OrderBy.DESC).Build();
        //}
    }
}