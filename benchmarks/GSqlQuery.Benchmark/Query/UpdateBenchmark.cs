using BenchmarkDotNet.Attributes;
using GSqlQuery.Benchmarks.Data;
using GSqlQuery.Default;
using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Benchmarks.Query
{
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public abstract class UpdateBenchmark : CreateStaments
    {
        protected User _user;

        public UpdateBenchmark() : base()
        {
            _user = new User();
        }

        [Benchmark]
        public IWhere<User, UpdateQuery<User>> GenerateWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where().Equal(x => x.Id, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where().Between(x => x.Id, 1, 2).Build();
        }

        [Benchmark]
        public IQuery GenerateGroupWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where().BeginGroup().Equal(x => x.Id, 1).CloseGroup().Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where().Like(x => x.Name, "23").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where().IsNull(x => x.Name).Build();
        }

        [Benchmark]
        public IWhere<User, UpdateQuery<User>> GenerateWhereQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where();
        }

        [Benchmark]
        public IQuery GenerateQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where().Equal(x => x.Id, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where().Between(x => x.Id, 1, 2).Build();
        }

        [Benchmark]
        public IQuery GenerateGroupWhereQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where().BeginGroup().Equal(x => x.Id, 1).CloseGroup().Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where().Like(x => x.Name, "23").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where().IsNull(x => x.Name).Build();
        }
    }

    public class Update : UpdateBenchmark
    {
        private readonly IEnumerable<int> _ids;
        public Update() : base()
        {
            _ids = Enumerable.Range(0, 1);
        }

        [Benchmark]
        public IQuery GenerateInWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where()
                       .In(x => x.Id, _ids)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQuery()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where()
                       .In(x => x.Id, _ids)
                       .AndEqual(x => x.Name, "nombre")
                       .OrBetween(x => x.Id, 1, 10)
                       .AndIsNull(x => x.LastName)
                       .AndNotLike(x => x.Email, ".gob")
                       .Build();
        }


        [Benchmark]
        public IQuery GenerateInWhereQueryByEntity()
        {
            return User.Update(_statements, x => x.Id, 1)
                       .Set(x => x.Name, "Test")
                       .Set(x => x.LastName, "LastTest")
                       .Set(x => x.Email, "guigalmen@hotmail.com")
                       .Set(x => x.IsActive, true)
                       .Where().In(x => x.Id, _ids).Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQueryByEntity()
        {
            return _user.Update(_statements, x => x.Id)
                       .Set(x => x.Name)
                       .Set(x => x.LastName)
                       .Set(x => x.Email)
                       .Set(x => x.IsActive)
                       .Where()
                       .In(x => x.Id, _ids)
                       .AndEqual(x => x.Name, "nombre")
                       .OrBetween(x => x.Id, 1, 10)
                       .AndIsNull(x => x.LastName)
                       .AndNotLike(x => x.Email, ".gob")
                       .Build();
        }

    }
}
