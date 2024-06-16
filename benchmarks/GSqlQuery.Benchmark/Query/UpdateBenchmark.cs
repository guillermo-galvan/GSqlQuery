using BenchmarkDotNet.Attributes;
using GSqlQuery.Benchmarks.Data;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Benchmarks.Query
{
    public class Update : CreateStaments
    {
        protected User _user;

        public Update() : base()
        {
            _user = new User();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return User.Update(_queryOptions, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return User.Update(_queryOptions, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where()
                       .Equal(x => x.Id, 1)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateQueryByEntity()
        {
            return _user.Update(_queryOptions, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQueryByEntity()
        {
            return _user.Update(_queryOptions, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where()
                       .Equal(x => x.Id, 1)
                       .Build();
        }
    }
}