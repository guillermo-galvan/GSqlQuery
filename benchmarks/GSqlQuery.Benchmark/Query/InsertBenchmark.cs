using BenchmarkDotNet.Attributes;
using GSqlQuery.Benchmarks.Data;

namespace GSqlQuery.Benchmarks.Query
{
    public class Insert : CreateStaments
    {
        protected User _user;

        public Insert() : base()
        {
            _user = new User();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return _user.Insert(_queryOptions).Build();
        }


        [Benchmark]
        public IQuery GenerateQuery_Entity()
        {
            return User.Insert(_queryOptions, _user).Build();
        }
    }
}