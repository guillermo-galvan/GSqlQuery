using BenchmarkDotNet.Attributes;
using GSqlQuery.Benchmarks.Data;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Benchmarks.Query
{
    public abstract class DeleteBenchmark : CreateStaments
    {
        public DeleteBenchmark() : base()
        {
        }

        [Benchmark]
        public IWhere<User, DeleteQuery<User>> GenerateWhereQuery()
        {
            return User.Delete(_statements).Where();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return User.Delete(_statements).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return User.Delete(_statements).Where().Equal(x => x.Id, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQuery()
        {
            return User.Delete(_statements).Where().Between(x => x.Id, 1, 2).Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQuery()
        {
            return User.Delete(_statements).Where().Like(x => x.Name, "23").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQuery()
        {
            return User.Delete(_statements).Where().IsNull(x => x.Name).Build();
        }
    }

    public class Delete : DeleteBenchmark
    {
        private readonly IEnumerable<int> _ids;
        public Delete() : base()
        {
            _ids = Enumerable.Range(0, 1);
        }

        [Benchmark]
        public IQuery GenerateInWhereQuery()
        {
            return User.Delete(_statements).Where().In(x => x.Id, _ids).Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQuery()
        {
            return User.Delete(_statements).Where()
                       .In(x => x.Id, _ids)
                       .AndEqual(x => x.Name, "nombre")
                       .OrBetween(x => x.Id, 1, 10)
                       .AndIsNull(x => x.LastName)
                       .AndNotLike(x => x.Email, ".gob")
                       .Build();
        }
    }
}
