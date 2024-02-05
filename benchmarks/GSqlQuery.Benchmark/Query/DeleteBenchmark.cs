using BenchmarkDotNet.Attributes;
using GSqlQuery.Benchmarks.Data;

namespace GSqlQuery.Benchmarks.Query
{
    public class Delete : CreateStaments
    {
        private User _user;
        public Delete() : base()
        {
            _user = new User(1, "name", "lastName", "test@gmail.com", true);
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return User.Delete(_formats).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return User.Delete(_formats).Where().Equal(x => x.Id, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateQuery_withEntity()
        {
            return User.Delete(_formats, _user).Build();
        }
    }
}