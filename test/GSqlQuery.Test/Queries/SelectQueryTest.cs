using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class SelectQueryTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly Equal<Test1, int> _equal;
        private readonly QueryOptions _queryOptions;
        private readonly ClassOptions _classOptions;
        private uint _parameterId = 0;

        public SelectQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions[nameof(Test1.Id)];
            var classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _columnAttribute);
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            _equal = new Equal<Test1, int>(classOptionsTupla.ClassOptions, new DefaultFormats(), 1, null, ref expression);
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            SelectQuery<Test1> query = new SelectQuery<Test1>("query", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _queryOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Table);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>("query", _classOptions.FormatTableName.Table, null, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>("query", null, null, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>("query", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], null));
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>(null, _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Should_Retrieve_From_The_Cache_Equal(long filmId)
        {
            var select = Film.Select(_queryOptions).Where().Equal(x => x.FilmId, filmId).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values);
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Single(parameterDetails);
            Assert.IsType<long>(parameterDetails.First().Value);
            Assert.Equal(filmId, parameterDetails.First().Value);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 5)]
        public void Should_Retrieve_From_The_Cache_AndEqual(long filmId, byte languageId)
        {
            var select = Film.Select(_queryOptions).Where().Equal(x => x.FilmId, filmId).AndEqual(x => x.LanguageId, languageId).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values);
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Equal(2, parameterDetails.Count());
            Assert.IsType<long>(parameterDetails.First().Value);
            Assert.Equal(filmId, parameterDetails.First().Value);
            Assert.IsType<byte>(parameterDetails.Last().Value);
            Assert.Equal(languageId, parameterDetails.Last().Value);
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        [InlineData("test4")]
        public void Should_Retrieve_From_The_Cache_Like(string title)
        {
            var select = Film.Select(_queryOptions).Where().Like(x => x.Title, title).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values);
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Single(parameterDetails);
            Assert.IsType<string>(parameterDetails.First().Value);
            Assert.Equal(title, parameterDetails.First().Value);
        }

        [Theory]
        [InlineData("test1", "test2")]
        [InlineData("test2", "test3")]
        [InlineData("test3", "test4")]
        [InlineData("test4", "test5")]
        public void Should_Retrieve_From_The_Cache_AndLike(string title, string description)
        {
            var select = Film.Select(_queryOptions).Where().Like(x => x.Title, title).AndLike(x => x.Description, description).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values);
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Equal(2, parameterDetails.Count());
            Assert.IsType<string>(parameterDetails.First().Value);
            Assert.Equal(title, parameterDetails.First().Value);
            Assert.IsType<string>(parameterDetails.Last().Value);
            Assert.Equal(description, parameterDetails.Last().Value);
        }

        [Theory]
        [InlineData(1, 12)]
        [InlineData(2, 13)]
        [InlineData(3, 14)]
        [InlineData(4, 15)]
        public void Should_Retrieve_From_The_Cache_Between(long initial, long final)
        {
            var select = Film.Select(_queryOptions).Where().Between(x => x.FilmId, initial, final).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values);
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Equal(2, parameterDetails.Count());
            Assert.IsType<long>(parameterDetails.First().Value);
            Assert.Equal(initial, parameterDetails.First().Value);
            Assert.IsType<long>(parameterDetails.Last().Value);
            Assert.Equal(final, parameterDetails.Last().Value);
        }

        [Theory]
        [InlineData(1, 12, 24, 35)]
        [InlineData(2, 13, 25, 36)]
        [InlineData(3, 14, 26, 37)]
        [InlineData(4, 15, 27, 38)]
        public void Should_Retrieve_From_The_Cache_AndBetween(long initial, long final, byte initialAnd, byte finalAnd)
        {
            var select = Film.Select(_queryOptions).Where().Between(x => x.FilmId, initial, final).AndBetween(x => x.LanguageId, initialAnd, finalAnd).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values).ToArray();
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Equal(4, parameterDetails.Length);
            Assert.IsType<long>(parameterDetails[0].Value);
            Assert.Equal(initial, parameterDetails[0].Value);
            Assert.IsType<long>(parameterDetails[1].Value);
            Assert.Equal(final, parameterDetails[1].Value);
            Assert.IsType<byte>(parameterDetails[2].Value);
            Assert.Equal(initialAnd, parameterDetails[2].Value);
            Assert.IsType<byte>(parameterDetails[3].Value);
            Assert.Equal(finalAnd, parameterDetails[3].Value);
        }


        [Theory]
        [InlineData(new long[] { 1, 2, 3, 4, 5 })]
        [InlineData(new long[] { 6, 7, 8, 9, 10 })]
        [InlineData(new long[] { 11, 12, 13, 14, 15 })]
        [InlineData(new long[] { 16, 17, 18, 19, 20 })]
        public void Should_Retrieve_From_The_Cache_In(long[] filmIds)
        {
            var select = Film.Select(_queryOptions).Where().In(x => x.FilmId, filmIds).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values);
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Equal(filmIds.Length, parameterDetails.Count());
            Assert.All(parameterDetails, item => Assert.IsType<long>(item.Value));
            Assert.Equal(filmIds, parameterDetails.Select(item => (long)item.Value).ToArray());
        }

        [Theory]
        [InlineData(new long[] { 1, 2, 3, 4, 5 }, new byte[] { 16, 17, 18, 19, 20 })]
        [InlineData(new long[] { 6, 7, 8, 9, 10 }, new byte[] { 11, 12, 13, 14, 15 })]
        [InlineData(new long[] { 11, 12, 13, 14, 15 }, new byte[] { 6, 7, 8, 9, 10 })]
        [InlineData(new long[] { 16, 17, 18, 19, 20 }, new byte[] { 1, 2, 3, 4, 5 })]
        public void Should_Retrieve_From_The_Cache_AndIn(long[] filmIds, byte[] languageIds)
        {
            var select = Film.Select(_queryOptions).Where().In(x => x.FilmId, filmIds).AndIn(x => x.LanguageId, languageIds).Build();

            Assert.NotNull(select.Criteria);
            Assert.NotEmpty(select.Criteria);

            var parameterDetails = select.Criteria.SelectMany(x => x.Values).ToArray();
            Assert.NotNull(parameterDetails);
            Assert.NotEmpty(parameterDetails);
            Assert.Equal(filmIds.Length + languageIds.Length, parameterDetails.Length);

            // Validar los filmIds
            for (int i = 0; i < filmIds.Length; i++)
            {
                Assert.IsType<long>(parameterDetails[i].Value);
                Assert.Equal(filmIds[i], parameterDetails[i].Value);
            }

            // Validar los languageIds
            for (int i = 0; i < languageIds.Length; i++)
            {
                Assert.IsType<byte>(parameterDetails[filmIds.Length + i].Value);
                Assert.Equal(languageIds[i], parameterDetails[filmIds.Length + i].Value);
            }
        }
    }
}