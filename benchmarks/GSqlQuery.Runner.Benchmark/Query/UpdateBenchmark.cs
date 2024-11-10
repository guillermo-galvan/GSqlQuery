using BenchmarkDotNet.Attributes;
using GSqlQuery.Runner.Benchmark.Data;
using GSqlQuery.Runner.Benchmark.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Runner.Benchmark.Query
{
    public abstract class UpdateBenchmark : BenchmarkBase
    {
        protected Film _film;

        public UpdateBenchmark() : base()
        {
            _film = new Film();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now)
                       .Where().Equal(x => x.FilmId, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQuery()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now)
                       .Where().Between(x => x.LastUpdate, DateTime.Now.AddDays(-30), DateTime.Now).Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQuery()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now)
                       .Where().Like(x => x.Title, "Test").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQuery()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now)
                       .Where().IsNull(x => x.Description).Build();
        }

        [Benchmark]
        public IQuery GenerateQueryByEntity()
        {
            return _film.Update(_connections, x => x.FilmId)
                       .Set(x => x.Title)
                       .Set(x => x.Description)
                       .Set(x => x.RentalDuration)
                       .Set(x => x.LastUpdate).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQueryByEntity()
        {
            return _film.Update(_connections, x => x.FilmId)
                       .Set(x => x.Title)
                       .Set(x => x.Description)
                       .Set(x => x.RentalDuration)
                       .Set(x => x.LastUpdate)
                       .Where().Equal(x => x.FilmId, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQueryByEntity()
        {
            return _film.Update(_connections, x => x.FilmId)
                       .Set(x => x.Title)
                       .Set(x => x.Description)
                       .Set(x => x.RentalDuration)
                       .Set(x => x.LastUpdate)
                       .Where().Between(x => x.LastUpdate, DateTime.Now.AddDays(-30), DateTime.Now).Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQueryByEntity()
        {
            return _film.Update(_connections, x => x.FilmId)
                       .Set(x => x.Title)
                       .Set(x => x.Description)
                       .Set(x => x.RentalDuration)
                       .Set(x => x.LastUpdate)
                       .Where().Like(x => x.Title, "23").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQueryByEntity()
        {
            return _film.Update(_connections, x => x.FilmId)
                       .Set(x => x.Title)
                       .Set(x => x.Description)
                       .Set(x => x.RentalDuration)
                       .Set(x => x.LastUpdate)
                       .Where().IsNull(x => x.Description).Build();
        }
    }

    public class Update : UpdateBenchmark
    {
        private readonly IEnumerable<long> _ids;
        public Update() : base()
        {
            _ids = Enumerable.Range(0, 10).Select(x => (long)x);
        }

        [Benchmark]
        public IQuery GenerateInWhereQuery()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now)
                       .Where()
                       .In(x => x.FilmId, _ids)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQuery()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now)
                       .Where()
                       .In(x => x.FilmId, _ids)
                       .AndEqual(x => x.Title, "nombre")
                       .OrBetween(x => x.LastUpdate, DateTime.Now.AddDays(-30), DateTime.Now)
                       .AndIsNull(x => x.Description)
                       .AndNotLike(x => x.SpecialFeatures, "Test")
                       .Build();
        }


        [Benchmark]
        public IQuery GenerateInWhereQueryByEntity()
        {
            return Film.Update(_connections, x => x.FilmId, 1)
                       .Set(x => x.Title, "Test")
                       .Set(x => x.Description, "LastTest")
                       .Set(x => x.RentalDuration, 5)
                       .Set(x => x.LastUpdate, DateTime.Now)
                       .Where().In(x => x.FilmId, _ids).Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQueryByEntity()
        {
            return _film.Update(_connections, x => x.FilmId)
                       .Set(x => x.Title)
                       .Set(x => x.Description)
                       .Set(x => x.RentalDuration)
                       .Set(x => x.LastUpdate)
                       .Where()
                       .In(x => x.FilmId, _ids)
                       .AndEqual(x => x.Title, "Test")
                       .OrBetween(x => x.LastUpdate, DateTime.Now.AddDays(-30), DateTime.Now)
                       .AndIsNull(x => x.Description)
                       .AndNotLike(x => x.SpecialFeatures, ".gob")
                       .Build();
        }

    }
}
