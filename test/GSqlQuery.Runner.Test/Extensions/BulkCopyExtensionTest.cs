using System.Collections.Generic;
using Xunit;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Linq;

namespace GSqlQuery.Runner.Test.Extensions
{
    public class BulkCopyExtensionTest
    {
        [Fact]
        public void Throw_an_exception_if_null_parameter()
        {
            IEnumerable<Test1> test1s = null;

            Assert.Throws<InvalidOperationException>(() => BatchExtension.FillTable(test1s));

            test1s = Enumerable.Empty<Test1>();
            Assert.Throws<InvalidOperationException>(() => BatchExtension.FillTable(test1s));
        }

        [Fact]
        public void FillTable_ok()
        {
            IEnumerable<Test1> test1s = Enumerable.Range(0,100).Select(x => new Test1() 
            {
                Id = x,
                Create = DateTime.Now,
                IsTest = x %2 == 0,
                Name = $"{x}Name"
            });


            var dataTable = BatchExtension.FillTable(test1s);

            Assert.NotNull(dataTable);
            Assert.True(test1s.Count() == dataTable.Rows.Count);
        }

        [Fact]
        public void FillTable_nullable_column()
        {
            IEnumerable<Film> films = Enumerable.Range(0, 100).Select(x => new Film()
            {
                FilmId = x,
                Description = "Description",
                LanguageId = 1, 
                LastUpdate = DateTime.Now,
                Length = null,
                OriginalLanguageId = 1,
                Rating = "Rating",
                ReleaseYear = "ReleaseYear",
                RentalDuration = 125,
                RentalRate = 135,
                ReplacementCost = 134,
                SpecialFeatures = "N/A",
                Title = "Test"
            });


            var dataTable = BatchExtension.FillTable(films);

            Assert.NotNull(dataTable);
            Assert.True(films.Count() == dataTable.Rows.Count);
        }
    }
}
